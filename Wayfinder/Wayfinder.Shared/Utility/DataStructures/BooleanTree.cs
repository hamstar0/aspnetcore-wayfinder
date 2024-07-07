namespace Wayfinder.Shared.Utility.DataStructures;



public interface IBoolean<Context> {
	bool True( Context context );
}



public class BooleanTree<NodeData, NodeContext> :
		IEquatable<BooleanTree<NodeData, NodeContext>?>,
		IBoolean<NodeContext>
			where NodeData : IBoolean<NodeContext> {
	/*public static IEnumerable<IBoolean<NodeContext>> Flatten(
				BooleanTree<NodeData, NodeContext> tree ) {
		var flattened = tree.Children.SelectMany( node => {
            var subtree = node as BooleanTree<NodeData, NodeContext>;
			if( subtree is null ) {
				return [];
			}
			return BooleanTree<NodeData, NodeContext>.Flatten( subtree );
		} );

		return flattened.Concat(
			tree.Children.Where( node => node is not BooleanTree<NodeData, NodeContext> )
		);
    }*/



	public IList<IBoolean<NodeContext>> Children = new List<IBoolean<NodeContext>>();

	public bool IsAnd;



	public BooleanTree( bool isAnd ) {
		IsAnd = isAnd;
	}

	public override bool Equals( object? test ) {
		return test is not null
			&& test is BooleanTree<NodeData, NodeContext>
			&& Equals( (BooleanTree<NodeData, NodeContext>)test );
	}

	public bool Equals( BooleanTree<NodeData, NodeContext>? other ) {
		return other is not null
			&& IsAnd == other.IsAnd
			&& EqualityComparer<IList<IBoolean<NodeContext>>>.Default.Equals( Children, other.Children );
	}

	public override int GetHashCode() {
		return HashCode.Combine( Children, IsAnd );
	}


	public void Clear() {
		Children.Clear();
	}


	public bool True( NodeContext context ) {
		IEnumerable<IBoolean<NodeContext>> children = Children
			.Where( c => c is not BooleanTree<NodeData, NodeContext>
				|| ((BooleanTree<NodeData, NodeContext>)c).Children.Count > 0 );

		return IsAnd
			? children.All( b => b.True( context ) )
			: children.Any( b => b.True( context ) );
	}


	public void Add( IBoolean<NodeContext> child, bool isAnd ) {
		if( child is not BooleanTree<NodeData, NodeContext> || child is not NodeData ) {
			throw new ArgumentException();
		}

		if( IsAnd == isAnd ) {
			Children.Add( child );
		} else {
			var innerTree = new BooleanTree<NodeData, NodeContext>( IsAnd );
			innerTree.Children = Children;

			IsAnd = isAnd;
			Children = new List<IBoolean<NodeContext>> {
				innerTree,
				child
			};
		}
	}
}

