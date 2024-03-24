using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Data.Entries;
using Wayfinder.Shared.Libraries;


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
    public bool SubmitOnEditOnly { get; set; } = false;


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


    public Timeline<PlanOptionEntry> GetPlanTimeline() {
        return this.GetCurrentPlan().OptionTimeline;
    }


    public async Task Submit_UI_Async() {
        f
    }
}
