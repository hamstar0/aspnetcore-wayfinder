﻿using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Data.Entries;


namespace Wayfinder.Client.Components.Application.Editors;



public partial class PlanEditor {
    //[Inject]
    //public IJSRuntime Js { get; set; } = null!;

    //[Inject]
    //public ClientDataAccess Data { get; set; } = null!;


    [Parameter, EditorRequired]
    public bool CanCreate { get; set; }

    [Parameter, EditorRequired]
    public bool CanEdit { get; set; }


    [Parameter]
    public PlanEntry? EditPlan { get; set; } = null;

    private PlanEntry CreatePlan = new PlanEntry();



    public PlanEntry GetCurrentPlan() {
        if( this.CanEdit && this.EditPlan is not null ) {
            return this.EditPlan;
        }
        if( this.CanCreate ) {
            return this.CreatePlan;
        }
        throw new Exception( "No PlanEntry available." );
    }
}
