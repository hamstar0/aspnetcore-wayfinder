using System.Text.Json.Serialization;
using Wayfinder.Shared.Utility.DataStructures;
using Wayfinder.Shared.Utility.Timeline.Data;


namespace Wayfinder.Shared.DataEntries.Descriptor;



public class DescriptorFactsEntry :
			TimelineEntry<TimelineEventDataEntry>,
			IBoolean<DescriptorFactsBooleanContext> {
	public DescriptorFactsEntry( IEnumerable<TimelineEventEntry<TimelineEventDataEntry>> events )
				: base( events ) {
	}

	public DescriptorFactsEntry( long id, IEnumerable<TimelineEventEntry<TimelineEventDataEntry>> events )
				: base( id, events ) {
	}


	public bool True( DescriptorFactsBooleanContext context ) {
	}
}



public class DescriptorFactsBooleanContext {
}