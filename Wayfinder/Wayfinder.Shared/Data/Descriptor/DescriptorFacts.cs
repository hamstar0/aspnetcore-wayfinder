using System.Text.Json.Serialization;
using Wayfinder.Shared.Utility;


namespace Wayfinder.Shared.Data.Entries.Descriptor;



public class DescriptorFactsEntry :
            TimelineEntry<DescriptorDataEntry>,
            IBoolean<DescriptorFactsBooleanContext> {
    public DescriptorFactsEntry( IEnumerable<TimelineEventEntry<DescriptorDataEntry>> events )
                : base( events ) {
    }

    public DescriptorFactsEntry( long id, IEnumerable<TimelineEventEntry<DescriptorDataEntry>> events )
                : base( id, events ) {
    }


    public bool True( DescriptorFactsBooleanContext context ) {
    }
}



public class DescriptorFactsBooleanContext {
}