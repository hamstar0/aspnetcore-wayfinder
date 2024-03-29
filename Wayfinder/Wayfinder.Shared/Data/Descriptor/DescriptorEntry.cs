using Wayfinder.Shared.Libraries;


namespace Wayfinder.Shared.Data.Entries.Descriptor;



public class DescriptorConditionsTree : BooleanTree<DataTimelineEntry, DataTimelineBooleanContext> {
	public DescriptorConditionsTree( bool isAnd ) : base( isAnd ) { }
}



public class DescriptorEntry : IEquatable<DescriptorEntry> {
	//[Key]
	public long Id { get; set; }

	public DataTimelineEntry Facts { get; set; } = null!;

	public DescriptorConditionsTree Conditions { get; set; } = null!;



	public bool ShallowEquals( DescriptorEntry other, bool skipRefs ) {
		if( this == other ) { return true; }

		if( this.Id != other.Id ) { return false; }
		if( !skipRefs && this.Facts != other.Facts ) { return false; }
		if( !skipRefs && this.Conditions != other.Conditions ) { return false; }
		return true;
	}

	public bool Equals( DescriptorEntry? other ) {
		if( other is null ) { return false; }
		if( !this.ShallowEquals(other, true) ) { return false; }
		if( !this.Facts.Equals(other.Facts) ) { return false; }
		if( !this.Conditions.Equals(other.Conditions) ) { return false; }
		return true;
	}
}
