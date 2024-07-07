using System;
using System.Text.Json.Serialization;


namespace Wayfinder.Shared.DataEntries;


public class PlanEntry {
	public long Id { get; private set; }

	[JsonIgnore]
	public bool IsAssignedId { get; private set; } = false;

	public string? Name = null;

	public GoalEntry Goal;

	public ISet<PlanOptionEntry> OptionsPool;

	public TimelineEntry<PlanOptionEntry> OptionTimeline;



	public PlanEntry(
				string? name,
				GoalEntry goal,
				ISet<PlanOptionEntry> optionsPool,
				TimelineEntry<PlanOptionEntry> optionTimeline ) {
		this.Name = name;
		this.Goal = goal;
		this.OptionsPool = optionsPool;
		this.OptionTimeline = optionTimeline;
	}

	public PlanEntry(
				long id,
				string? name,
				GoalEntry goal,
				ISet<PlanOptionEntry> optionsPool,
				TimelineEntry<PlanOptionEntry> optionTimeline ) {
		this.Id = id;
		this.IsAssignedId = true;
		this.Name = name;
		this.Goal = goal;
		this.OptionsPool = optionsPool;
		this.OptionTimeline = optionTimeline;
	}
}

