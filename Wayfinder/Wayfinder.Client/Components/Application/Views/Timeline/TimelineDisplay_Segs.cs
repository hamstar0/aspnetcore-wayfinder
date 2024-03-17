using System;
using Wayfinder.Shared.Libraries;


namespace Wayfinder.Client.Components.Application.Views.Timeline;


public partial class TimelineDisplay<TimelineEventType, TimelineDataType>
            where TimelineEventType : TimelineEvent<TimelineDataType> {
    public int ComputeSegOffset( DateTime startTime ) {
        return (int)this.GetOffsetXOfTimestamp( startTime );
    }

    public int ComputeSegWidth( DateTime startTime, DateTime endTime ) {
        return (int)this.GetWidthOfTimespan( endTime - startTime );
    }


    public IList<TimelineEventType> GetVisibleScheduleEvents() {
        double maxWidth = TimelineDisplay<TimelineEventType, TimelineDataType>.MaxElementWidth;

        return this.Timeline.GetEventsBetween(
            this.ViewTimeStart,
            this.ViewTimeStart + this.GetTimespanOfOffsetX( maxWidth )
        );
    }
}
