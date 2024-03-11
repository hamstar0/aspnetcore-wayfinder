namespace Wayfinder.Shared.Data.Entries;


public class TermEntry : IEquatable<TermEntry>, IComparable, IComparable<TermEntry> {
	public long Id { get; set; }

	public string Term { get; set; } = null!;

	public TermEntry? Context { get; set; } = null!;

	public TermEntry? Alias { get; set; } = null;



    public bool Equals( TermEntry? other ) {
		if( other is null ) { return false; }
        if( this == other ) { return true; }

        if( this.Id != other.Id ) { return false; }
        if( this.Term != other.Term ) { return false; }
        if( this.Context != other.Context ) { return false; }
		if( this.Alias != other.Alias ) { return false; }
        return true;
    }

    public int CompareTo( object? test ) {
		if( test is not TermEntry ) {
			return 1;
		}
		return this.CompareTo( (TermEntry)test );
    }
    public int CompareTo( TermEntry? test ) {
        if( test is null ) {
            return 1;
        }

        return this.SafeCompareTo( test, null );
    }

    private int SafeCompareTo( TermEntry test, ISet<TermEntry>? scanned ) {
        if( scanned == null ) {
            scanned = new HashSet<TermEntry> { this };
        } else if( scanned.Contains(this) ) {
            throw new Exception( "Circular reference" );
        } else {
            scanned.Add( this );
        }

        int cmp = this.Term.CompareTo( test.Term );
        if( cmp != 0 ) {
			return cmp;
		}

        if( this.Context is not null ) {
			if( test.Context is null ) {
                return 1;
            }
            cmp = this.Context.SafeCompareTo( test.Context, scanned );
            if( cmp != 0 ) {
                return cmp;
            }
        } else {
			if( test.Context is not null ) {
				return -1;
			}
		}

        if( this.Alias is not null ) {
			if( test.Alias is null ) {
                return 1;
            }
            cmp = this.Alias.SafeCompareTo( test.Alias, scanned );
            if( cmp != 0 ) {
                return cmp;
            }
        } else {
			if( test.Alias is not null ) {
				return -1;
			}
		}

		return 0;
	}

    public bool DeepCompare( string term, TermEntry? context ) {
        if( this.Term != term ) {
            return false;
        }

        TermEntry? alias = this;
        while( alias.Alias is not null ) {
            alias = alias.Alias;

            if( alias.Term != term ) {
                return false;
            }
        }

        if( context is not null ) {
            if( this.Context is null ) {
                return false;
            }
            if( this.Context.DeepCompare(context.Term, context.Context) ) {
                return false;
            }
        } else if( this.Context is not null ) {
            return false;
        }

        return true;
    }
}
