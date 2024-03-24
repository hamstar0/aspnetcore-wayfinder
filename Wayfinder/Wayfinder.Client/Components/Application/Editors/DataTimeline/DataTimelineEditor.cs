using System;
using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Data.Entries.Descriptor.DescriptorDataTypes;
using Wayfinder.Client.Data;
using Wayfinder.Client.Components.Application.Views.Timeline;
using Wayfinder.Shared.Libraries;
using Wayfinder.Shared.Data;


namespace Wayfinder.Client.Components.Application.Editors.DataTimeline;


public partial class DataTimelineEditor {
    public delegate Task<bool> OnSubmitSchedule( DataTimelineEntry schedule, bool isEdit );



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
	public DataTimelineEntry? EditSchedule { get; set; } = null;

	private DataTimelineEntry CreateSchedule = new DataTimelineEntry();


	[Parameter]
    public bool RangeDataTypesOnly { get; set; } = false;
    
    [Parameter]
    public bool SubmitOnEditOnly { get; set; } = false;

    [Parameter, EditorRequired]
    public OnSubmitSchedule OnSubmit { get; set; } = null!;


    [Parameter]
    public DateTime ViewTimeStart { get; set; } = DateTime.Now;



	private bool IsDrawingSeg = false;

    private TimelineEvent<DescriptorDataEntry>? CurrentDrawSeg = null;

    private DescriptorDataType CurrentDrawSegDataType = DescriptorDataType.Scalar;

    private DescriptorDataEntry? CurrentDrawSegDataValue = null;


    private TimelineDisplay<DescriptorDataEntry> DisplayComponent = null!;



    private void SetCurrentSegValue( DescriptorDataType dataType, object value ) {
        switch( this.CurrentDrawSegDataType ) {
            case DescriptorDataType.Scalar:
                this.CurrentDrawSegDataValue = new ScalarDDataEntry( (double)value );
                break;
        }
    }


    public double GetWidthOfTimespan( TimeSpan span ) {
        return span.TotalSeconds / this.DisplayComponent.ZoomScale;
    }

    public double GetOffsetXOfTimestamp( DateTime time ) {
        return this.GetWidthOfTimespan( time - this.ViewTimeStart );
    }

    public TimeSpan GetTimespanOfOffsetX( double x ) {
        return new TimeSpan( ticks: (long)(x * this.DisplayComponent.ZoomScale * (double)TimeSpan.TicksPerSecond) );
    }


    private DataTimelineEntry GetCurrentSchedule() {
        if( this.CanEdit ) {
            if( this.EditSchedule is null ) {
				throw new InvalidDataException( "No edit ScheduleEntry available." );
			}
			return this.EditSchedule;
		}
        if( this.CanCreate ) {
			if( this.CreateSchedule is null ) {
				throw new InvalidDataException( "No create ScheduleEntry available." );
			}
			return this.CreateSchedule;
		}
		throw new InvalidDataException( "ScheduleEditor cannot create or edit Schedules (!)" );
	}

	public bool CanEditOrCreate() {
		if( this.Disabled ) {
			return false;
		}
		if( this.CanEdit && this.EditSchedule is null ) {
            if( this.CanCreate && this.CreateSchedule is null ) {
                return false;
            }
		}
		return true;
	}

}
