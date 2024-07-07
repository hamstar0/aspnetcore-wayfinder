using System;
using Wayfinder.Client.Data;
using Wayfinder.Shared.Data;
using Wayfinder.Shared.DataEntries.Descriptor;
using Wayfinder.Shared.Utility;
using Wayfinder.Shared.Utility.Timeline.Data;


namespace Wayfinder.Client.Components.Application.Editors.DescriptorFacts;


public partial class DescriptorFactsEditor {
    private void DrawAt_Async( double x ) {
        this.IsDrawingSeg = true;

        if( this.CurrentDrawSegDataValue is null ) {
            return;
        }

        if( this.CurrentDrawSeg is null ) {
            this.CurrentDrawSeg = new TimelineEventEntry<TimelineDataEntry>(
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

        this.GetCurrentFacts().AddEvent( this.CurrentDrawSeg );

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
        if( this.CanEdit && this.Edit is not null ) {
			await this.AttemptEditSubmit_Async();
		} else if( this.CanCreate && this.Create_FactsEvents is not null ) {
			await this.AttemptCreateSubmit_Async();
		}
	}

    private async Task AttemptCreateSubmit_Async() {
        if( this.OnSubmit is null ) {
            return;
        }

        DescriptorFactsEntry newTimeline = await this.Data.CreateDescriptorFacts_Async(
			new ClientDataAccess.CreateDescriptorFactsParams( this.Create_FactsEvents )
		);
		if( await this.OnSubmit.Invoke(newTimeline, false) ) {
			return;
		}
	}

    private async Task AttemptEditSubmit_Async() {
        if( this.OnSubmit is null ) {
            throw new InvalidDataException( "Missing OnSubmit handler for edits" );
        }
        if( this.Edit is null ) {
            throw new InvalidDataException( "Missing EditDescriptorFacts" );
        }
        if( !await this.OnSubmit.Invoke(this.Edit, true) ) {
			this.Edit = await this.Data.AddDescriptorFactsEvents_Async(
				new ClientDataAccess.AddDescriptorFactsEventsParams(
                    this.Edit.Id,
					this.Edit.Events
				)
			);
		}
	}
}
