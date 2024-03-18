using System;


namespace Wayfinder.Shared.Data.Entries;


public class PlanEntry {
	public long Id { get; set; }

	public string? Name { get; set; } = null;

	public GoalEntry Goal { get; set; } = null!;

	public IList<PlanOptionEntry> Options { get; set; } = null!;
}

