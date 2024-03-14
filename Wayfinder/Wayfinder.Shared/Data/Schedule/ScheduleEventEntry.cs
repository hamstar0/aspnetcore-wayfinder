using System.Collections;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Libraries;


namespace Wayfinder.Shared.Data.Schedule;


public partial class ScheduleEventEntry : TimelineEvent<DescriptorDataEntry>
{
    public long Id { get; set; }


    public ScheduleEventEntry(long id, DateTime start, DateTime end, DescriptorDataEntry data)
                : base(start, end, data)
    {
        Id = id;
    }
}
