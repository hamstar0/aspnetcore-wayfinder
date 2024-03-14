using System.Collections;
using Wayfinder.Shared.Libraries;


namespace Wayfinder.Shared.Data.Entries.Descriptor;


public partial class ScheduleEventEntry : TimelineEvent<DescriptorDataEntry> {
	public long Id { get; set; }


	public ScheduleEventEntry( long id, DateTime start, DateTime end, DescriptorDataEntry data )
				: base( start, end, data ) {
		this.Id = id;
	}
}
