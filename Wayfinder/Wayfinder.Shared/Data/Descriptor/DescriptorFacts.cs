using System.Text.Json.Serialization;
using Wayfinder.Shared.Utility;


namespace Wayfinder.Shared.Data.Entries.Descriptor;



public class DescriptorFacts :
            TimelineEntry<DescriptorDataEntry>,
            IBoolean<DescriptorFactsBooleanContext> {
    public DescriptorFacts( IEnumerable<TimelineEventEntry<DescriptorDataEntry>> events )
                : base( events ) {
    }

    public DescriptorFacts( long id, IEnumerable<TimelineEventEntry<DescriptorDataEntry>> events )
                : base( id, events ) {
    }


    public bool True( DescriptorFactsBooleanContext context ) {
    }
}



public class DescriptorFactsBooleanContext {
}