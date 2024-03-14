using System;
using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Data.Entries.Descriptor.DescriptorDataTypes;
using Wayfinder.Client.Data;
using Wayfinder.Shared.Libraries;
using Wayfinder.Shared.Data.Schedule;


namespace Wayfinder.Client.Components.Application.Editors.Schedule;


public partial class ScheduleEditor {
    public delegate Task<bool> OnSubmitSchedule( ScheduleEntry schedule, bool isEdit );



    public readonly static int MaxElementWidth = 1920;



    //[Inject]
    //public IJSRuntime Js { get; set; } = null!;

    [Inject]
    public ClientDataAccess Data { get; set; } = null!;



    [Parameter]
    public bool Disabled { get; set; } = false;

	[Parameter, EditorRequired]
    public bool CanEdit { get; set; }

	[Parameter, EditorRequired]
    public bool CanCreate { get; set; }


	[Parameter]
	public ScheduleEntry? EditSchedule { get; set; } = null;

	private DescriptorEntry CreateScheduleFor = new DescriptorEntry();

	private Timeline<ScheduleEventEntry, DescriptorDataEntry> CreateScheduleTimeline
        = new Timeline<ScheduleEventEntry, DescriptorDataEntry>();


	[Parameter]
    public bool RangeDataTypesOnly { get; set; } = false;
    
    [Parameter]
    public bool SubmitOnEditOnly { get; set; } = false;

    [Parameter, EditorRequired]
    public OnSubmitSchedule OnSubmit { get; set; } = null!;


    [Parameter]
    public DateTime ViewTimeStart { get; set; } = DateTime.Now;

    private double ZoomScale = 1d;  // pixel per second

    private double ScrollBaseAmount = 30d;



	private bool IsDrawingSeg = false;

    private ScheduleEventEntry? CurrentDrawSeg = null;

    private DescriptorDataType CurrentDrawSegDataType = DescriptorDataType.Scalar;

    private DescriptorDataEntry? CurrentDrawSegDataValue = null;



    private void SetCurrentSegValue( DescriptorDataType dataType, object value ) {
        switch( this.CurrentDrawSegDataType ) {
            case DescriptorDataType.Scalar:
                this.CurrentDrawSegDataValue = new ScalarDDataEntry( (double)value );
                break;
        }
    }


    public double GetWidthOfTimespan( TimeSpan span ) {
        return span.TotalSeconds / this.ZoomScale;
    }

    public double GetOffsetXOfTimestamp( DateTime time ) {
        return this.GetWidthOfTimespan( time - this.ViewTimeStart );
    }

    public TimeSpan GetTimespanOfOffsetX( double x ) {
        return new TimeSpan( ticks: (long)(x * this.ZoomScale * (double)TimeSpan.TicksPerSecond) );
    }


    private Timeline<ScheduleEventEntry, DescriptorDataEntry> GetCurrentScheduleTimeline() {
        if( this.CanEdit ) {
            if( this.EditSchedule is null ) {
				throw new InvalidDataException( "No edit ScheduleEntry available." );
			}
			return this.EditSchedule;
		}
        if( this.CanCreate ) {
			if( this.CreateScheduleTimeline is null ) {
				throw new InvalidDataException( "No create Timeline available." );
			}
			return this.CreateScheduleTimeline;
		}
		throw new InvalidDataException( "ScheduleEditor cannot create or edit Schedules (!)" );
	}

	public bool CanEditOrCreate() {
		if( this.Disabled ) {
			return false;
		}
		if( this.CanEdit && this.EditSchedule is null ) {
            if( this.CanCreate && this.CreateScheduleTimeline is null ) {
                return false;
            }
		}
		return true;
	}

}
