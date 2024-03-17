using Wayfinder.Shared.Libraries.BooleanTree;


namespace Wayfinder.Shared.Data.Entries.Descriptor;



//public enum DescriptorDataType {
//    None = 0,
//    Term = 1,
//    Scalar = 2,
//    LocationRange = 4,
//    LocationArea = 8,
//    LocationAddress = 16,
//    //FixedSchedule,
//    //DynamicLocation,
//    //DynamicLocations,
//    //DynamicState,
//    //Dynamic
//}


public class DescriptorEntry : IEquatable<DescriptorEntry>,
            IComparable,
            IComparable<DescriptorEntry>,
            IBoolean {
    //[Key]
    public long Id { get; set; }

    //[Comment( "Subject" )]
    public TermEntry TermSubj { get; set; } = null!;

    //[Comment( "Relationship nature" )]
    public TermEntry TermRel { get; set; } = null!;

    //[Comment( "Descriptor" )]
    //public TermEntry? TermDesc { get; set; } = null;

    public bool IsFact { get; set; }

    public bool IsImpossible { get; set; }

    public bool IsPlaceholder { get; set; }

    public ScheduleEntry Schedule { get; set; } = null!;

    public BooleanTree<DescriptorEntry>? Conditions { get; set; }



    public bool ShallowEquals( DescriptorEntry other, bool skipRefs ) {
        if( this == other ) { return true; }

        if( this.Id != other.Id ) { return false; }
        if( !this.TermSubj.Equals(other.TermSubj) ) { return false; }
        if( !this.TermRel.Equals(other.TermRel) ) { return false; }
        if( this.IsFact != other.IsFact ) { return false; }
        if( this.IsImpossible != other.IsImpossible ) { return false; }
        if( this.IsPlaceholder != other.IsPlaceholder ) { return false; }
        if( !skipRefs && this.Schedule != other.Schedule ) { return false; }
        if( !skipRefs && this.Conditions != other.Conditions ) { return false; }
        return true;
    }

    public bool Equals( DescriptorEntry? other ) {
        if( other is null ) { return false; }
        if( !this.ShallowEquals(other, true) ) { return false; }
        if( !this.Schedule.Equals(other.Schedule) ) { return false; }
        if( !this.Conditions?.Equals(other.Conditions) ?? other.Conditions is not null ) {
            return false;
        }
        return true;
    }

    public int CompareTo( object? obj ) {
        if( obj is not DescriptorEntry ) {
            throw new ArgumentException();
        }

        return this.CompareTo( (DescriptorEntry)obj );
    }

    public int CompareTo( DescriptorEntry? test ) {
        if( test is null ) {
            throw new ArgumentException();
        }

        if( this == test ) {
            return 0;
        }

        if( this.IsFact ) {
            if( !test.IsFact ) {
                return 1;
            }
        } else if( test.IsFact ) {
            return -1;
        }

        int termCmp = this.TermSubj.CompareTo( test.TermSubj );
        if( termCmp != 0 ) {
            return termCmp;
        }

        termCmp = this.TermRel.CompareTo( test.TermRel );
        if( termCmp != 0 ) {
            return termCmp;
        }

        if( this.Schedule != test.Schedule ) {
            if( this.Schedule.GetHashCode() > test.Schedule.GetHashCode() ) {
                return 1;
            } else if( this.Schedule.GetHashCode() < test.Schedule.GetHashCode() ) {
                return 0;
            }
        }

        if( this.Conditions is null ) {
            if( test.Conditions is not null ) { return 1; }
        } else if( test.Conditions is not null ) {
            return -1;
        }

        if( this.Conditions is not null && test.Conditions is not null ) {
            if( this.Conditions != test.Conditions ) {
                if( this.Conditions.GetHashCode() > test.Conditions.GetHashCode() ) {
                    return 1;
                } else if( this.Conditions.GetHashCode() < test.Conditions.GetHashCode() ) {
                    return -1;
                }
            }
        }

        return 0;
    }

    public bool True() {
        return this.Conditions is null || this.Conditions.True();
    }
}
