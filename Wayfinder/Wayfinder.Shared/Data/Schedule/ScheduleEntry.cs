using System.Collections;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Libraries;


namespace Wayfinder.Shared.Data.Schedule;



public partial class ScheduleEntry : Timeline<ScheduleEventEntry, DescriptorDataEntry> {
	//public DescriptorEntry For { get; set; }   // 1:1

	public ScheduleEntry() : base() { }

	public ScheduleEntry( IEnumerable<ScheduleEventEntry> events ) : base( events ) { }

	public ScheduleEntry( long id,  IEnumerable<ScheduleEventEntry> events ) : base( id, events ) { }
}



public partial class ScheduleEventEntry : TimelineEvent<DescriptorDataEntry> { 
	public ScheduleEventEntry( DateTime start, DateTime end, DescriptorDataEntry data )
			: base( start, end, data ) {
	}

	public ScheduleEventEntry( long id, DateTime start, DateTime end, DescriptorDataEntry data )
			: base( id, start, end, data ) {
	}
}
