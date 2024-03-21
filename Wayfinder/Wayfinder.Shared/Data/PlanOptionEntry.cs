﻿using System;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Libraries;
using Wayfinder.Shared.Libraries.BooleanTree;


namespace Wayfinder.Shared.Data.Entries;


public class PlanOptionEntry {
	//[Key]
	public long Id { get; set; }

	public DescriptorEntry OptionFor { get; set; } = null!;

	public string? Name { get; set; } = null;

    public bool IsChosen { get; set; }

	public long MinimumEnactingDuration { get; set; }

	public DescriptorEntry ConditionsAndAction { get; set; } = null!;

	//public Timeline<bool> AvailableTimeWindows { get; set; } = null!;

	public TimelineEvent<bool> ScheduledTimeWindow { get; set;} = null!;



	public Timeline<DescriptorDataEntry> GetAvailableTimeWindows( long minTime, long maxTime ) {
	}
}