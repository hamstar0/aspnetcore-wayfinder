﻿using System;


namespace Wayfinder.Client.Components.Standard.Timeline;


public partial class TimelineDisplay<TimelineDataType> {
    public int ComputeSegOffset( DateTime startTime ) {
        return (int)this.GetOffsetXOfTimestamp( startTime );
    }

    public int ComputeSegWidth( DateTime startTime, DateTime endTime ) {
        return (int)this.GetWidthOfTimespan( endTime - startTime );
    }

    
    public IList<TimelineEventEntry<TimelineDataType>> GetVisibleTimelineEvents() {
        double maxWidth = TimelineDisplay<TimelineDataType>.MaxElementWidth;

        return this.Timeline.GetEventsBetween(
            this.ViewTimeStart,
            this.ViewTimeStart + this.GetTimespanOfOffsetX( maxWidth )
        );
    }
}
