using System;
using Microsoft.AspNetCore.Components.Web;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Utility;


namespace Wayfinder.Client.Components.Application.Editors.DescriptorFacts;


public partial class DescriptorFactsEditor {
    private async Task OnMouseDown_UI_Async( MouseEventArgs evt ) {
        if( !this.CanEditOrCreate() ) { return; }

        double x = evt.OffsetX;

        foreach( TimelineEventEntry<DescriptorDataEntry> timelineEvt in this.GetCurrentFacts().Events ) {
            double evtStartX = this.GetOffsetXOfTimestamp( timelineEvt.StartTime );
            if( x < evtStartX ) { f
                break;
            }

            double evtEndX = this.GetOffsetXOfTimestamp( timelineEvt.EndTime );
            if( x > evtEndX ) {
                continue;
            }

            if( await this.OnMouseDownOverSeg_Async(x, timelineEvt) ) {
                return;
            } else {
                break;
            }
        }

        this.DrawAt_Async( x );
    }

    private async Task<bool> OnMouseDownOverSeg_Async( double x, TimelineEventEntry<DescriptorDataEntry> evt ) {
return !this.IsDrawingSeg;
    }


    private async Task OnMouseUp_UI_Async( MouseEventArgs evt ) {
		if( !this.CanEditOrCreate() ) { return; }

		await this.FinishingDrawing_Async( evt.OffsetX );
    }
}
