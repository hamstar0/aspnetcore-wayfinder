using System;
using Wayfinder.Shared.Libraries;


namespace Wayfinder.Client.Components.Standard.Timeline;


public partial class TimelineDisplay<TimelineDataType> {
    public int ComputeSegOffset( DateTime startTime ) {
        return (int)this.GetOffsetXOfTimestamp( startTime );
    }

    public int ComputeSegWidth( DateTime startTime, DateTime endTime ) {
        return (int)this.GetWidthOfTimespan( endTime - startTime );
    }

    
    public IList<TimelineEvent<TimelineDataType>> GetVisibleScheduleEvents() {
        double maxWidth = TimelineDisplay<TimelineDataType>.MaxElementWidth;

        return this.Timeline.GetEventsBetween(
            this.ViewTimeStart,
            this.ViewTimeStart + this.GetTimespanOfOffsetX( maxWidth )
        );
    }
}
