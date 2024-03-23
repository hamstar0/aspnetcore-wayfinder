﻿namespace Wayfinder.Shared.Libraries.BooleanTree;



public interface IBoolean<Context> {
	bool True( Context context );
}



public class BooleanTree<NodeData, NodeContext>
		: IEquatable<BooleanTree<NodeData, NodeContext>?>
			where NodeData : IBoolean<NodeContext> {
    public IList<IBoolean<NodeContext>> Children = new List<IBoolean<NodeContext>>();

	public bool IsAnd;



	public BooleanTree( bool isAnd ) {
		this.IsAnd = isAnd;
	}

	public override bool Equals( object? test ) {
        return test is not null
			&& test is BooleanTree<NodeData, NodeContext>
			&& this.Equals( (BooleanTree<NodeData, NodeContext>)test );
    }

    public bool Equals( BooleanTree<NodeData, NodeContext>? other ) {
        return other is not null
			&& this.IsAnd == other.IsAnd
			&& EqualityComparer<IList<IBoolean<NodeContext>>>.Default.Equals( this.Children, other.Children );
    }

    public override int GetHashCode() {
        return HashCode.Combine( this.Children, this.IsAnd );
    }


    public bool True( NodeContext context ) {
		IEnumerable<IBoolean<NodeContext>> children = this.Children
			.Where( c => c is not BooleanTree<NodeData, NodeContext>
				|| ((BooleanTree<NodeData, NodeContext>)c).Children.Count > 0 );

		return this.IsAnd
			? children.All( b => b.True(context) )
			: children.Any( b => b.True(context) );
	}


	public void Add( IBoolean<NodeContext> child, bool isAnd ) {
		if( child is not BooleanTree<NodeData, NodeContext> || child is not NodeData ) {
			throw new ArgumentException();
		}

		if( this.IsAnd == isAnd ) {
			this.Children.Add( child );
		} else {
			var innerTree = new BooleanTree<NodeData, NodeContext>( this.IsAnd );
			innerTree.Children = this.Children;

			this.IsAnd = isAnd;
			this.Children = new List<IBoolean<NodeContext>> {
				(IBoolean<NodeContext>)innerTree,
				child
			};
		}
	}
}

