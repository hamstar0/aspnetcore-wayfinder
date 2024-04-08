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

	public DescriptorFactsEntry Actions;

	public DescriptorConditionsTreeEntry Conditions;



	public PlanOptionEntry(
				string? name,
				long minimumEnactingDuration,
                DescriptorFactsEntry actions,
				DescriptorConditionsTreeEntry conditions ) {
		this.Name = name;
		this.MinimumEnactingDuration = minimumEnactingDuration;
		this.Actions = actions;
		this.Conditions = conditions;
	}

	public PlanOptionEntry(
				long id,
				string? name,
				long minimumEnactingDuration,
                DescriptorFactsEntry actions,
				DescriptorConditionsTreeEntry conditions ) {
		this.Id = id;
		this.IsAssignedId = true;
		this.Name = name;
		this.MinimumEnactingDuration = minimumEnactingDuration;
		this.Actions = actions;
		this.Conditions = conditions;
	}
}