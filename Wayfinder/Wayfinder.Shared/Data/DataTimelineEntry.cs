using System.Collections;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Utility;


namespace Wayfinder.Shared.Data;



public class DataTimelineBooleanContext {
}



public partial class DataTimelineEntry : TimelineEntry<DescriptorDataEntry>, IBoolean<DataTimelineBooleanContext> {
    //public DescriptorEntry For { get; set; }   // 1:1

    public DataTimelineEntry() : base() { }

    public DataTimelineEntry( IEnumerable<TimelineEventEntry<DescriptorDataEntry>> events )
            : base( events ) { }

    public DataTimelineEntry( long id, IEnumerable<TimelineEventEntry<DescriptorDataEntry>> events )
            : base( id, events ) { }


	public bool True( DataTimelineBooleanContext context ) {
	}
}
