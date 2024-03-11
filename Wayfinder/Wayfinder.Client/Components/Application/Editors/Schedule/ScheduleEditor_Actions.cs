using System;
using Wayfinder.Client.Data;
using Wayfinder.Shared.Data.Entries.Descriptor;


namespace Wayfinder.Client.Components.Application.Editors.Schedule;


public partial class ScheduleEditor {
    private void DrawAt_Async( double x ) {
        this.IsDrawingSeg = true;

        if( this.CurrentDrawSeg is null ) {
            this.CurrentDrawSeg = new ScheduleEventEntry {
                For = this.Schedule,
                StartTime = this.ViewTimeStart + this.GetTimespanOfOffsetX(x),
                EndTime = this.ViewTimeStart + this.GetTimespanOfOffsetX(x + 1d),
                State = this.CurrentDrawSegDataValue
            };

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

        this.Schedule.AddEvent( this.CurrentDrawSeg );

        this.CurrentDrawSeg = null;
        this.IsDrawingSeg = false;

        if( this.SubmitOnEditOnly ) {
            await this.AttemptSubmit_Async();
        }
    }


    public void Scroll( double amt ) {
        this.ViewTimeStart += this.GetTimespanOfOffsetX( amt );
    }

    public void Zoom_UI( double amt ) {
        if( this.Disabled ) { return; }

        this.ZoomScale = amt;
    }


    public void ScrollLeft_UI() {
        if( this.Disabled ) { return; }

        this.Scroll( this.ZoomScale * -this.ScrollBaseAmount );
    }
    public void ScrollRight_UI() {
        if( this.Disabled ) { return; }

        this.Scroll( this.ZoomScale * this.ScrollBaseAmount );
    }

    private async Task AttemptSubmit_UI_Async() {
        if( this.Disabled ) { return; }

        await this.AttemptSubmit_Async();
    }

    private async Task AttemptSubmit_Async() {
        if( !await this.OnEditSubmit(this.Schedule) ) {
            await this.Data.CreateSchedule_Async(
                new ClientDataAccess.CreateScheduleParams(this.Schedule.For, this.Schedule.Events.ToList())
            );
        }
    }
}
