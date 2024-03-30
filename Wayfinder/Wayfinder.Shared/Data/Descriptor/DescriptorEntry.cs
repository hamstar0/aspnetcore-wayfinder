using System.Text.Json.Serialization;
using Wayfinder.Shared.Utility;


namespace Wayfinder.Shared.Data.Entries.Descriptor;



public class DescriptorConditionsTree : BooleanTree<DataTimelineEntry, DataTimelineBooleanContext> {
	public DescriptorConditionsTree( bool isAnd ) : base( isAnd ) { }
}



public class DescriptorEntry : IEquatable<DescriptorEntry> {
	//[Key]
	public long Id { get; private set; }

	[JsonIgnore]
	public bool IsAssignedId { get; private set; } = false;

	public DataTimelineEntry Facts;

	public DescriptorConditionsTree Conditions;



	public DescriptorEntry() {
		this.Facts = new DataTimelineEntry();
		this.Conditions = new DescriptorConditionsTree( true );
	}

	public DescriptorEntry( long id, DataTimelineEntry facts, DescriptorConditionsTree conditions ) {
		this.Id = id;
		this.IsAssignedId = true;
		this.Facts = facts;
		this.Conditions = conditions;
	}


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
