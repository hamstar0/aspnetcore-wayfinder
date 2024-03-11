namespace Wayfinder.Shared.Libraries.BooleanTree;



public interface IBoolean : IComparable {
    bool True();
}



public class BooleanTree<T> where T : IBoolean, IComparable {
    public IList<IBoolean> Children = new List<IBoolean>();

    public bool IsAnd;



    public BooleanTree( bool isAnd ) {
        this.IsAnd = isAnd;
    }

    //public int CompareTo( BooleanTree<T>? other ) {
    public int CompareTo( object? other ) {
        if( other is null || other is not BooleanTree<T> ) {
            return 1;
        }

        var test = (BooleanTree<T>)other;
        return this.True()
            ? (test.True() ? 0 : 1)
            : (test.True() ? -1 : 0);
    }

    public bool True() {
        return this.Children.Count > 0
            && this.IsAnd
                ? this.Children.All( b => b.True() )
                : this.Children.Any( b => b.True() );
    }


    public void Add( IBoolean child, bool isAnd ) {
        if( child is not BooleanTree<T> || child is not T ) {
            throw new ArgumentException();
        }

        if( this.IsAnd == isAnd ) {
            this.Children.Add( child );
        } else {
            var innerTree = new BooleanTree<T>( this.IsAnd );
            innerTree.Children = this.Children;

            this.IsAnd = isAnd;
            this.Children = new List<IBoolean> {
                (IBoolean)innerTree,
                child
            };
        }
    }
}

