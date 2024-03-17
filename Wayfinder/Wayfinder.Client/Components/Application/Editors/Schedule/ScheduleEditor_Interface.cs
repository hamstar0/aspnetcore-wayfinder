using System;
using Microsoft.AspNetCore.Components.Web;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Libraries;


namespace Wayfinder.Client.Components.Application.Editors.Schedule;


public partial class ScheduleEditor {
    private async Task OnMouseDown_UI_Async( MouseEventArgs evt ) {
        if( !this.CanEditOrCreate() ) { return; }

        double x = evt.OffsetX;

        foreach( TimelineEvent<DescriptorDataEntry> schEvt in this.GetCurrentSchedule().Events ) {
            double evtStartX = this.GetOffsetXOfTimestamp( schEvt.StartTime );
            if( x < evtStartX ) {
                break;
            }

            double evtEndX = this.GetOffsetXOfTimestamp( schEvt.EndTime );
            if( x > evtEndX ) {
                continue;
            }

            if( await this.OnMouseDownOverSeg_Async(x, schEvt) ) {
                return;
            } else {
                break;
            }
        }

        this.DrawAt_Async( x );
    }

    private async Task<bool> OnMouseDownOverSeg_Async( double x, TimelineEvent<DescriptorDataEntry> evt ) {
return !this.IsDrawingSeg;
    }


    private async Task OnMouseUp_UI_Async( MouseEventArgs evt ) {
		if( !this.CanEditOrCreate() ) { return; }

		await this.FinishingDrawing_Async( evt.OffsetX );
    }
}
