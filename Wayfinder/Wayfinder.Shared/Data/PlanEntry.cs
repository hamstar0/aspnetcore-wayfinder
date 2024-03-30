using System;
using System.Text.Json.Serialization;
using Wayfinder.Shared.Utility;


namespace Wayfinder.Shared.Data.Entries;


public class PlanEntry {
	public long Id { get; private set; }

	[JsonIgnore]
	public bool IsAssignedId { get; private set; } = false;

	public string? Name = null;

	public GoalEntry Goal;

	public IList<PlanOptionEntry> OptionsPool;

	public Timeline<PlanOptionEntry> OptionTimeline;



	public PlanEntry( GoalEntry goal ) {
		this.Goal = goal;
		this.OptionsPool = new List<PlanOptionEntry>();
		this.OptionTimeline = new Timeline<PlanOptionEntry>();
	}

	public PlanEntry( long id, string? name, GoalEntry goal, IList<PlanOptionEntry> optionsPool, Timeline<PlanOptionEntry> optionTimeline ) {
		this.Id = id;
		this.IsAssignedId = true;
		this.Name = name;
		this.Goal = goal;
		this.OptionsPool = optionsPool;
		this.OptionTimeline = optionTimeline;
	}
}

