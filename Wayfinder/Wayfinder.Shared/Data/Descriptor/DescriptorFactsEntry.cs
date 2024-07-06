using System.Text.Json.Serialization;
using Wayfinder.Shared.Utility;
using Wayfinder.Shared.Utility.Timeline.Data;


namespace Wayfinder.Shared.Data.Entries.Descriptor;



public class DescriptorFactsEntry :
            TimelineEntry<TimelineDataEntry>,
            IBoolean<DescriptorFactsBooleanContext> {
    public DescriptorFactsEntry( IEnumerable<TimelineEventEntry<TimelineDataEntry>> events )
                : base( events ) {
    }

    public DescriptorFactsEntry( long id, IEnumerable<TimelineEventEntry<TimelineDataEntry>> events )
                : base( id, events ) {
    }


    public bool True( DescriptorFactsBooleanContext context ) {
    }
}



public class DescriptorFactsBooleanContext {
}