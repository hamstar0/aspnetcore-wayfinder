using System;
using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Data.Schedule;
using Wayfinder.Shared.Libraries;


namespace Wayfinder.Client.Components.Application.Views.Schedule;


public partial class ScheduleDisplay {
    public readonly static int MaxElementWidth = 1920;



    //[Inject]
    //public IJSRuntime Js { get; set; } = null!;

    //[Inject]
    //public ClientDataAccess Data { get; set; } = null!;



    [Parameter, EditorRequired]
    public ScheduleEntry Schedule { get; set; } = null!;


    [Parameter]
    public DateTime ViewTimeStart { get; set; } = DateTime.Now;


    private double ZoomScale = 1d;  // pixel per second

    private double ScrollBaseAmount = 30d;



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
