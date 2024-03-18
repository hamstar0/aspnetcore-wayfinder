using System;
using Wayfinder.Shared.Data.Entries.Descriptor;


namespace Wayfinder.Shared.Data.Entries;


public class PlanOptionEntry {
	//[Key]
	public long Id { get; set; }

	public DescriptorEntry Needed { get; set; } = null!;

	public DescriptorEntry Fulfills { get; set; } = null!;

	//public DescriptorDataEntry Event { get; set; } = null!;
}