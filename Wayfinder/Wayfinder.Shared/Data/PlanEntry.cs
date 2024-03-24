using System;
using Wayfinder.Shared.Libraries;


namespace Wayfinder.Shared.Data.Entries;


public class PlanEntry {
	public long Id { get; set; }

	public string? Name { get; set; } = null;

	public GoalEntry Goal { get; set; } = null!;

	public ISet<PlanOptionEntry> Options { get; set; } = null!;

	public Timeline<PlanOptionEntry> OptionTimeline { get; set; } = null!;
}

