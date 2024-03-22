using System.Collections;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Libraries;
using Wayfinder.Shared.Libraries.BooleanTree;


namespace Wayfinder.Shared.Data;



public class ScheduleValidator : TimelineEvent<bool> {
	public ScheduleValidator( DateTime start, DateTime end, bool data )
			: base( start, end, data ) { }
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
