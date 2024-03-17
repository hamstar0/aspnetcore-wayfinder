using System.Collections;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Libraries;


namespace Wayfinder.Shared.Data.Schedule;



public partial class ScheduleEntry : Timeline<DescriptorDataEntry> {
	//public DescriptorEntry For { get; set; }   // 1:1

	public ScheduleEntry() : base() { }

	public ScheduleEntry( IEnumerable<TimelineEvent<DescriptorDataEntry>> events )
			: base( events ) { }

	public ScheduleEntry( long id,  IEnumerable<TimelineEvent<DescriptorDataEntry>> events )
			: base( id, events ) { }
}
