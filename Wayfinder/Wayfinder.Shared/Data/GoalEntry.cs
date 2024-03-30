using System;
using System.Text.Json.Serialization;
using Wayfinder.Shared.Data.Entries.Descriptor;


namespace Wayfinder.Shared.Data.Entries;


public class GoalEntry {
	//[Key]
	public long Id { get; private set; }

	[JsonIgnore]
	public bool IsAssignedId { get; private set; } = false;

	public string Name;

	public string? Description = null;

	public DescriptorConditionsTree Conditions;



	public GoalEntry( string name ) {
		this.Name = name;
		this.Conditions = new DescriptorConditionsTree( true );
	}

	public GoalEntry( long id, string name, string? description, DescriptorConditionsTree conditions ) {
		this.Id = id;
		this.IsAssignedId = true;
		this.Name = name;
		this.Description = description;
		this.Conditions = conditions;
	}
}
