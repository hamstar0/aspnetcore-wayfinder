using System;
using Wayfinder.Client.Data;
using Wayfinder.Shared.Data;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Utility;


namespace Wayfinder.Client.Components.Application.Editors.DataTimeline;


public partial class DataTimelineEditor {
    private void DrawAt_Async( double x ) {
        this.IsDrawingSeg = true;

        if( this.CurrentDrawSegDataValue is null ) {
            return;
        }

        if( this.CurrentDrawSeg is null ) {
            this.CurrentDrawSeg = new TimelineEventEntry<DescriptorDataEntry>(
                start: this.ViewTimeStart + this.GetTimespanOfOffsetX(x),
                end: this.ViewTimeStart + this.GetTimespanOfOffsetX(x + 1d),
                data: this.CurrentDrawSegDataValue
            );

            return;
        }

        DateTime drawTime = this.ViewTimeStart + this.GetTimespanOfOffsetX( x );

        if( this.CurrentDrawSeg.StartTime > drawTime ) {
            this.CurrentDrawSeg.StartTime = drawTime;
        } else if( this.CurrentDrawSeg.EndTime > drawTime ) {
            this.CurrentDrawSeg.EndTime = drawTime;
        }
    }

    private async Task FinishingDrawing_Async( double x ) {
        if( this.CurrentDrawSeg is null ) {
            return;
        }

        this.GetCurrentTimeline().AddEvent( this.CurrentDrawSeg );

        this.CurrentDrawSeg = null;
        this.IsDrawingSeg = false;

        if( this.SubmitOnEditOnly && this.CanEdit ) {
			await this.AttemptSubmit_Async();
		}
    }


    private async Task AttemptSubmit_UI_Async() {
        if( this.CanEditOrCreate() ) { return; }

		await this.AttemptSubmit_Async();
	}

    private async Task AttemptSubmit_Async() {
        if( this.CanEdit && this.EditDataTimeline is not null ) {
			await this.AttemptEditSubmit_Async();
		} else if( this.CanCreate && this.CreateDataTimeline is not null ) {
			await this.AttemptCreateSubmit_Async();
		}
	}

    private async Task AttemptCreateSubmit_Async() {
        if( this.OnSubmit is null ) {
            return;
        }

		DataTimelineEntry newTimeline = await this.Data.CreateDataTimeline_Async(
			new ClientDataAccess.CreateDataTimelineParams( this.CreateDataTimeline.Events )
		);
		if( await this.OnSubmit.Invoke(newTimeline, false) ) {
			return;
		}
	}

    private async Task AttemptEditSubmit_Async() {
        if( this.OnSubmit is null ) {
            throw new InvalidDataException( "Missing OnSubmit handler for edits" );
        }
        if( this.EditDataTimeline is null ) {
            throw new InvalidDataException( "Missing EditDataTimeline" );
        }
        if( !await this.OnSubmit.Invoke(this.EditDataTimeline, true) ) {
			this.EditDataTimeline = await this.Data.AddDataTimelineEvents_Async(
				new ClientDataAccess.AddDataTimelineEventsParams(
                    this.EditDataTimeline.Id,
					this.EditDataTimeline.Events
				)
			);
		}
	}
}
