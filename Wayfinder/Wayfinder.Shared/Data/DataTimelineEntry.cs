using System.Collections;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Libraries;
using Wayfinder.Shared.Libraries.BooleanTree;

namespace Wayfinder.Shared.Data;



public class DataTimelineBooleanContext {
}



public partial class DataTimelineEntry : Timeline<DescriptorDataEntry>, IBoolean<DataTimelineBooleanContext> {
    //public DescriptorEntry For { get; set; }   // 1:1

    public DataTimelineEntry() : base() { }

    public DataTimelineEntry(IEnumerable<TimelineEvent<DescriptorDataEntry>> events)
            : base(events) { }

    public DataTimelineEntry(long id, IEnumerable<TimelineEvent<DescriptorDataEntry>> events)
            : base(id, events) { }


	public bool True( DataTimelineBooleanContext context ) {
	}
}
