using System;
using System.Collections.Generic;
using Wayfinder.Shared.Utility.Timeline;

namespace Wayfinder.Shared.DataEntries.Plan;



public class PlanOptionTimelineEntry : TimelineEntry {
	public PlanOptionTimelineEntry( long id, IEnumerable<TimelineEventEntry> events )
				: base( id, events ) {
	}
}
