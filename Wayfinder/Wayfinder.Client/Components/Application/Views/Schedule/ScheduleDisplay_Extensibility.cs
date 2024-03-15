using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;


namespace Wayfinder.Client.Components.Application.Views.Schedule;


public partial class ScheduleDisplay {
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
