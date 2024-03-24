using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Data.Entries;


namespace Wayfinder.Client.Components.Application.Editors;



public partial class PlanOptionEditor {
    //[Inject]
    //public IJSRuntime Js { get; set; } = null!;

    //[Inject]
    //public ClientDataAccess Data { get; set; } = null!;


    [Parameter]
    public PlanEntry? Plan { get; set; } = null;


    [Parameter, EditorRequired]
    public PlanOptionEntry Option { get; set; } = null!;



	private async Task ToggleCurrentPlanOption_UI_Async() {
        if( this.Plan is null ) {
            return;
        }

        if( this.Plan.Options.Contains(this.Option) ) {
			this.Plan.Options.Remove( this.Option );
		} else {
			this.Plan.Options.Add( this.Option );
		}
	}
}
