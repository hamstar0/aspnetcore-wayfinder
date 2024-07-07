using System.Text.Json.Serialization;
using Wayfinder.Shared.Utility;


namespace Wayfinder.Shared.DataEntries.Descriptor;



public class DescriptorEntry : IEquatable<DescriptorEntry> {
	//[Key]
	public long Id { get; private set; }

	[JsonIgnore]
	public bool IsAssignedId { get; private set; } = false;

	public DescriptorFactsEntry Facts;

	public DescriptorConditionsTreeEntry Conditions;



	public DescriptorEntry( DescriptorFactsEntry facts, DescriptorConditionsTreeEntry conditions ) {
		this.Facts = facts;
		this.Conditions = conditions;
	}

	public DescriptorEntry( long id, DescriptorFactsEntry facts, DescriptorConditionsTreeEntry conditions ) {
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
		if( !this.ShallowEquals( other, true ) ) { return false; }
		if( !this.Facts.Equals( other.Facts ) ) { return false; }
		if( !this.Conditions.Equals( other.Conditions ) ) { return false; }
		return true;
	}
}
