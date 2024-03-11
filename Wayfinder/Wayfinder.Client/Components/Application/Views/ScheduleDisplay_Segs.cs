﻿using System;
using Wayfinder.Shared.Data.Entries.Descriptor;


namespace Wayfinder.Client.Components.Application.Views;


public partial class ScheduleDisplay {
    public int ComputeSegOffset( ScheduleEventEntry evt ) {
        return (int)this.GetOffsetXOfTimestamp( evt.StartTime );
    }

    public int ComputeSegWidth( ScheduleEventEntry evt ) {
        return (int)this.GetWidthOfTimespan( evt.EndTime - evt.StartTime );
    }


    public IList<ScheduleEventEntry> GetVisibleScheduleEvents() {
        return this.Schedule.GetEventsBetween(
            this.ViewTimeStart,
            this.ViewTimeStart + this.GetTimespanOfOffsetX( (double)ScheduleDisplay.MaxElementWidth )
        );
    }
}