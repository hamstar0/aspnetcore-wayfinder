using System;
using System.Text.Json.Serialization;


namespace Wayfinder.Shared.DataEntries.Plan;


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
		Name = name;
		Goal = goal;
		OptionsPool = optionsPool;
		OptionTimeline = optionTimeline;
	}

	public PlanEntry(
				long id,
				string? name,
				GoalEntry goal,
				ISet<PlanOptionEntry> optionsPool,
				TimelineEntry<PlanOptionEntry> optionTimeline ) {
		Id = id;
		IsAssignedId = true;
		Name = name;
		Goal = goal;
		OptionsPool = optionsPool;
		OptionTimeline = optionTimeline;
	}
}

