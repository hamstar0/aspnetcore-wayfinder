using System.Collections;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Libraries;
using Wayfinder.Shared.Libraries.BooleanTree;

namespace Wayfinder.Shared.Data;



public class ScheduleValidator {
    public bool Validate( ScheduleEntry schedule ) {
    }
}



public partial class ScheduleEntry : Timeline<DescriptorDataEntry>, IBoolean<ScheduleValidator> {
    //public DescriptorEntry For { get; set; }   // 1:1

    public ScheduleEntry() : base() { }

    public ScheduleEntry(IEnumerable<TimelineEvent<DescriptorDataEntry>> events)
            : base(events) { }

    public ScheduleEntry(long id, IEnumerable<TimelineEvent<DescriptorDataEntry>> events)
            : base(id, events) { }


	public bool True( ScheduleValidator context ) {
	}
}
