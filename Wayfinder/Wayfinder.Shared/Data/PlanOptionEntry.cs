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

	public DataTimelineEntry Actions;

	public DescriptorConditionsTree Conditions;



	public PlanOptionEntry() {
		this.Actions = new DataTimelineEntry();
		this.Conditions = new DescriptorConditionsTree( true );
	}

	public PlanOptionEntry(
			long id,
			string? name,
			long minimumEnactingDuration,
			DataTimelineEntry actions,
			DescriptorConditionsTree conditions ) {
		this.Id = id;
		this.IsAssignedId = true;
		this.Name = name;
		this.MinimumEnactingDuration = minimumEnactingDuration;
		this.Actions = actions;
		this.Conditions = conditions;
	}
}