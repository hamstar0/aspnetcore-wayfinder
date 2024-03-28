using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Data.Entries;


namespace Wayfinder.Client.Components.Application.Editors;



public partial class PlanOptionEditor {
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
    public PlanEntry? Plan { get; set; } = null;


    [Parameter, EditorRequired]
	public PlanOptionEntry EditOption { get; set; } = null!;



	private async Task ToggleCurrentPlanOption_UI_Async() {
        if( this.Plan is null ) {
            throw new Exception( "No plan to edit options for." );
        }

        if( this.Plan.Options.Contains(this.EditOption) ) {
            this.Plan.Options.Remove( this.EditOption );
        } else {
            this.Plan.Options.Add( this.EditOption );
        }
    }
}
