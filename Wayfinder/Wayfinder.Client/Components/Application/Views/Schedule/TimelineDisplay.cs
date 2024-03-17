﻿using System;
using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Data.Schedule;
using Wayfinder.Shared.Libraries;


namespace Wayfinder.Client.Components.Application.Views.Schedule;


public partial class TimelineDisplay<TimelineEventType, TimelineDataType>
            where TimelineEventType : TimelineEvent<TimelineDataType> {
    public readonly static int MaxElementWidth = 1920;



    //[Inject]
    //public IJSRuntime Js { get; set; } = null!;

    //[Inject]
    //public ClientDataAccess Data { get; set; } = null!;


    [Parameter, EditorRequired]
    public Timeline<TimelineEventType, TimelineDataType> Timeline { get; set; } = null!;


    [Parameter]
    public DateTime ViewTimeStart { get; set; } = DateTime.Now;


    public double ZoomScale { get; private set; } = 1d;  // pixel per second

    private double ScrollBaseAmount = 30d;


    [Parameter]
    public Timeline<TimelineEvent<bool>, bool>? ConditionWindows { get; set; } = null;

    private bool ShowConditions = true;




    public double GetWidthOfTimespan( TimeSpan span ) {
        return span.TotalSeconds / this.ZoomScale;
    }

    public double GetOffsetXOfTimestamp( DateTime time ) {
        return this.GetWidthOfTimespan( time - this.ViewTimeStart );
    }

    public TimeSpan GetTimespanOfOffsetX( double x ) {
        return new TimeSpan( ticks: (long)(x * this.ZoomScale * (double)TimeSpan.TicksPerSecond) );
    }
}
