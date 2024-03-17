using System;
using Wayfinder.Client.Data;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Data.Schedule;
using Wayfinder.Shared.Libraries;


namespace Wayfinder.Client.Components.Application.Editors.Schedule;


public partial class ScheduleEditor {
    private void DrawAt_Async( double x ) {
        this.IsDrawingSeg = true;

        if( this.CurrentDrawSegDataValue is null ) {
            return;
        }

        if( this.CurrentDrawSeg is null ) {
            this.CurrentDrawSeg = new TimelineEvent<DescriptorDataEntry>(
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

        this.GetCurrentSchedule().AddEvent( this.CurrentDrawSeg );

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
        if( this.CanEdit && this.EditSchedule is not null ) {
			await this.AttemptEditSubmit_Async();
		} else if( this.CanCreate && this.CreateSchedule is not null ) {
			await this.AttemptCreateSubmit_Async();
		}
	}

    private async Task AttemptCreateSubmit_Async() {
        if( this.OnSubmit is null ) {
            return;
        }

		ScheduleEntry newSched = await this.Data.CreateSchedule_Async(
			new ClientDataAccess.CreateScheduleParams( this.CreateSchedule.Events )
		);
		if( await this.OnSubmit.Invoke(newSched, false) ) {
			return;
		}
	}

    private async Task AttemptEditSubmit_Async() {
        if( this.OnSubmit is null ) {
            throw new InvalidDataException( "Missing OnSubmit handler for edits" );
        }
        if( this.EditSchedule is null ) {
            throw new InvalidDataException( "Missing EditSchedule" );
        }
        if( !await this.OnSubmit.Invoke(this.EditSchedule, true) ) {
			this.EditSchedule = await this.Data.AddScheduleEvents_Async(
				new ClientDataAccess.AddScheduleEventsParams(
                    this.EditSchedule.Id,
					this.EditSchedule.Events
				)
			);
		}
	}
}
