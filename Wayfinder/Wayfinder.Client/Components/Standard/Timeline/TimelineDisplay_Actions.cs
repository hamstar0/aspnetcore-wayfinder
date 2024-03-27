using System;
using Wayfinder.Shared.Libraries;


namespace Wayfinder.Client.Components.Standard.Timeline;


public partial class TimelineDisplay<TimelineDataType> {
    public void Scroll( double amt ) {
        this.ViewTimeStart += this.GetTimespanOfOffsetX( amt );
    }

    public void Zoom_UI( double amt ) {
        //if( this.Disabled ) { return; }

        this.ZoomScale = amt;
    }


    public void ScrollLeft_UI() {
        //if( this.Disabled ) { return; }

        this.Scroll( this.ZoomScale * -this.ScrollBaseAmount );
    }
    public void ScrollRight_UI() {
        //if( this.Disabled ) { return; }

        this.Scroll( this.ZoomScale * this.ScrollBaseAmount );
    }
}
