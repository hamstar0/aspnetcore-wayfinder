using Wayfinder.Shared.Libraries;
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



public class DescriptorScheduleValidator : TimelineEvent<bool> {
	public DescriptorScheduleValidator( DateTime start, DateTime end, bool data )
			: base( start, end, data ) { }
}



public class DescriptorEntry : IEquatable<DescriptorEntry>, IBoolean<DescriptorScheduleValidator> {
	//[Key]
	public long Id { get; set; }

	//[Comment( "Subject" )]
	public TermEntry TermSubj { get; set; } = null!;

	//[Comment( "Relationship nature" )]
	public TermEntry TermRel { get; set; } = null!;

	//[Comment( "Descriptor" )]
	//public TermEntry? TermDesc { get; set; } = null;

	//public bool IsFact { get; set; }

	public bool IsImpossible { get; set; }

	//public bool IsPlaceholder { get; set; }

	public ScheduleEntry Schedule { get; set; } = null!;

	public BooleanTree<DescriptorEntry, DescriptorScheduleValidator> Conditions { get; set; } = null!;



	public bool ShallowEquals( DescriptorEntry other, bool skipRefs ) {
		if( this == other ) { return true; }

		if( this.Id != other.Id ) { return false; }
		if( !this.TermSubj.Equals(other.TermSubj) ) { return false; }
		if( !this.TermRel.Equals(other.TermRel) ) { return false; }
		//if( this.IsFact != other.IsFact ) { return false; }
		if( this.IsImpossible != other.IsImpossible ) { return false; }
		//if( this.IsPlaceholder != other.IsPlaceholder ) { return false; }
		if( !skipRefs && this.Schedule != other.Schedule ) { return false; }
		if( !skipRefs && this.Conditions != other.Conditions ) { return false; }
		return true;
	}

	public bool Equals( DescriptorEntry? other ) {
		if( other is null ) { return false; }
		if( !this.ShallowEquals(other, true) ) { return false; }
		if( !this.Schedule.Equals(other.Schedule) ) { return false; }
		if( !this.Conditions.Equals(other.Conditions) ) {
			return false;
		}
		return true;
	}

	public bool True( DescriptorScheduleValidator context ) {
	}
}
