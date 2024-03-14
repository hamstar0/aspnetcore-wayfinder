using System;


namespace Wayfinder.Shared.Data.Entries;


public class PlanEntry {
	public long Id { get; set; }

	public string Name { get; set; } = null!;

	public IList<PlanStepEntry> Steps { get; set; } = null!;
}

