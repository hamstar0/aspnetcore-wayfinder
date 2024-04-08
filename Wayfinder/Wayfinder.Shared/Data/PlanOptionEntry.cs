using System;
using System.Text.Json.Serialization;
using Wayfinder.Shared.Data.Entries.Descriptor;


namespace Wayfinder.Shared.Data.Entries;


public class PlanOptionEntry {
	//[Key]
	public long Id { get; private set; }

	[JsonIgnore]
	public bool IsAssignedId { get; private set; } = false;

	public string? Name;

	public long MinimumEnactingDuration;

	public DescriptorFacts Actions;

	public DescriptorConditionsTree Conditions;



	public PlanOptionEntry(
				string? name,
				long minimumEnactingDuration,
                DescriptorFacts actions,
				DescriptorConditionsTree conditions ) {
		this.Name = name;
		this.MinimumEnactingDuration = minimumEnactingDuration;
		this.Actions = actions;
		this.Conditions = conditions;
	}

	public PlanOptionEntry(
				long id,
				string? name,
				long minimumEnactingDuration,
                DescriptorFacts actions,
				DescriptorConditionsTree conditions ) {
		this.Id = id;
		this.IsAssignedId = true;
		this.Name = name;
		this.MinimumEnactingDuration = minimumEnactingDuration;
		this.Actions = actions;
		this.Conditions = conditions;
	}
}