using System;
using Wayfinder.Shared.Data.Schedule;


namespace Wayfinder.Client.Components.Application.Views.Schedule;


public partial class ScheduleDisplay {
    public int ComputeSegOffset( DateTime startTime ) {
        return (int)this.GetOffsetXOfTimestamp( startTime );
    }

    public int ComputeSegWidth( DateTime startTime, DateTime endTime ) {
        return (int)this.GetWidthOfTimespan( endTime - startTime );
    }


    public IList<ScheduleEventEntry> GetVisibleScheduleEvents() {
        return this.Schedule.GetEventsBetween(
            this.ViewTimeStart,
            this.ViewTimeStart + this.GetTimespanOfOffsetX( (double)ScheduleDisplay.MaxElementWidth )
        );
    }
}
