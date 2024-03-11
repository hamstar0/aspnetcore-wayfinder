using System;


namespace Wayfinder.Shared.Data.Entries;


public class PlanEntry {
	//[Key]
	public long Id { get; set; }

	public string Name { get; set; } = null!;

	public GoalEntry Goal { get; set; } = null!;

	//[InverseProperty( "Plan" )]
	//public ICollection<PlanStepEntry> Steps { get; set; } = null!;
}

