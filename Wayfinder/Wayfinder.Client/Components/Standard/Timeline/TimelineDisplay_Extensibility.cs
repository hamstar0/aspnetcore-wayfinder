using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Wayfinder.Shared.Utility;


namespace Wayfinder.Client.Components.Standard.Timeline;


public partial class TimelineDisplay<TimelineDataType> {
    [Parameter]
    public RenderFragment? AddedHeadButtonsPre { get; set; } = null;

    [Parameter]
    public RenderFragment? Footer { get; set; } = null;


    [Parameter]
    public Func<MouseEventArgs, Task>? OnMouseDown { get; set; } = null;

    [Parameter]
    public Func<MouseEventArgs, Task>? OnMouseUp { get; set; } = null;



    public async Task OnMouseDown_UI_Async( MouseEventArgs e ) {
        if( this.OnMouseDown is not null ) {
            await this.OnMouseDown( e );
        }
    }

    public async Task OnMouseUp_UI_Async( MouseEventArgs e ) {
        if( this.OnMouseUp is not null ) {
            await this.OnMouseUp( e );
        }
    }
}
