using System;
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

	public BooleanTree<DescriptorEntry> Needed { get; set; } = null!;

	public DescriptorEntry Action { get; set; } = null!;

	//public Timeline<bool> AvailableTimeWindows { get; set; } = null!;

	public TimelineEvent<bool> ScheduledTimeWindow { get; set;} = null!;



	public Timeline<DescriptorDataEntry> GetAvailableTimeWindows( long minTime, long maxTime ) {
		IDictionary<DescriptorEntry, Timeline<DescriptorDataEntry>> conditionWindows
			= this.OptionFor.GetConditionsEventsBetween( minTime, maxTime );
		Timeline<DescriptorDataEntry>? finalWindows = null;

		foreach( (var condition, var windows) in conditionWindows ) {
			if( !condition.True() ) {
				continue;
			}

			if( finalWindows is null ) {
				finalWindows = windows;
				continue;
			}

			finalWindows.Intersect( windows );
		}

		return finalWindows ?? new Timeline<DescriptorDataEntry>();
	}
}