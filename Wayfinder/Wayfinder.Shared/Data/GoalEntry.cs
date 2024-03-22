using System;
using Wayfinder.Shared.Data.Entries.Descriptor;


namespace Wayfinder.Shared.Data.Entries;


public class GoalEntry {
	//[Key]
	public long Id { get; set; }

	public string Name { get; set; } = null!;

	public string? Description { get; set; } = null;

	public DescriptorEntry ConditionsAndState { get; set; } = null!;
}
