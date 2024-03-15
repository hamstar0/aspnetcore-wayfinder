using System;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Data.Schedule;
using Wayfinder.Shared.Libraries;


namespace Wayfinder.Client.Components.Application.Editors.Schedule;


public partial class ScheduleEditor {
    public int ComputeSegOffset( TimelineEvent<DescriptorDataEntry> evt ) {
        return (int)this.GetOffsetXOfTimestamp( evt.StartTime );
    }

    public int ComputeSegWidth( TimelineEvent<DescriptorDataEntry> evt ) {
        return (int)this.GetWidthOfTimespan( evt.EndTime - evt.StartTime );
    }


    public IList<ScheduleEventEntry> GetVisibleScheduleEvents() {
        ScheduleEntry schedule = this.GetCurrentSchedule();

        return schedule.GetEventsBetween(
            this.ViewTimeStart,
            this.ViewTimeStart + this.GetTimespanOfOffsetX( (double)ScheduleEditor.MaxElementWidth )
        );
    }
}
