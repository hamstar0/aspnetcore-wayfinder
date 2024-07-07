using System;
using System.Text.Json.Serialization;
using Wayfinder.Shared.DataEntries.Descriptor;


namespace Wayfinder.Shared.DataEntries;


public class GoalEntry {
	//[Key]
	public long Id { get; private set; }

	[JsonIgnore]
	public bool IsAssignedId { get; private set; } = false;

	public string Name;

	public string? Description = null;

	public DescriptorConditionsTreeEntry Conditions;



	public GoalEntry( string name, string? description, DescriptorConditionsTreeEntry conditions ) {
		this.Name = name;
		this.Description = description;
		this.Conditions = conditions;
	}

	public GoalEntry( long id, string name, string? description, DescriptorConditionsTreeEntry conditions ) {
		this.Id = id;
		this.IsAssignedId = true;
		this.Name = name;
		this.Description = description;
		this.Conditions = conditions;
	}
}
