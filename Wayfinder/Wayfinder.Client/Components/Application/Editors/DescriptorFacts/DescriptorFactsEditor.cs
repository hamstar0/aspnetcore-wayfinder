using System;
using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Utility;
using Wayfinder.Shared.Data;
using Wayfinder.Client.Data;
using Wayfinder.Client.Components.Standard.Timeline;
using Wayfinder.Shared.Utility.Timeline.Data;
using Wayfinder.Shared.Utility.Timeline.Data.TimelineDataTypes;
using Wayfinder.Shared.DataEntries.Descriptor;


namespace Wayfinder.Client.Components.Application.Editors.DescriptorFacts;


public partial class DescriptorFactsEditor {
    public delegate Task<OverridesDefault> SubmitDescriptorFacts( DescriptorFactsEntry facts, bool isEdit );



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


    public bool IsModified => this.EditChanges is not null;


	[Parameter]
	public DescriptorFactsEntry? Edit { get; set; } = null;

	private DescriptorFactsEntry? EditChanges { get; set; } = null;


	private IEnumerable<TimelineEventEntry<TimelineEventDataEntry>> Create_FactsEvents
        = new List<TimelineEventEntry<TimelineEventDataEntry>>();


	[Parameter]
    public bool RangeDataTypesOnly { get; set; } = false;
    
    [Parameter]
    public bool SubmitOnEditOnly { get; set; } = false;

    [Parameter, EditorRequired]
    public SubmitDescriptorFacts OnSubmit { get; set; } = null!;


    [Parameter]
    public DateTime ViewTimeStart { get; set; } = DateTime.Now;



	private bool IsDrawingSeg = false;

    private TimelineEventEntry<TimelineEventDataEntry>? CurrentDrawSeg = null;

    private TimelineDataType CurrentDrawSegDataType = TimelineDataType.Scalar;

    private TimelineEventDataEntry? CurrentDrawSegDataValue = null;


    private TimelineDisplay<TimelineEventDataEntry> DisplayComponent = null!;



    private DescriptorFactsEntry GetCurrentFacts() {

    }


    private void SetCurrentSegValue( TimelineDataType dataType, object value ) {
        switch( this.CurrentDrawSegDataType ) {
            case TimelineDataType.Scalar:
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


	public bool CanEditOrCreate() {
		if( this.Disabled ) {
			return false;
		}
		if( this.CanEdit && this.Edit is null ) {
            return this.CanCreate;
		}
		return true;
	}

}
