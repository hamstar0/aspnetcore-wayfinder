using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Data.Entries;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Utility;


namespace Wayfinder.Client.Components.Application.Editors.Plan;



public partial class PlanOptionEditor {
	public delegate Task<OverridesDefault> PlanOptionSubmit( PlanOptionEntry option, bool isEdit );
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


    [Parameter]
	public PlanOptionEntry? EditOption { get; set; } = null;

	private Optional<string?> CreateOption_Name = new Optional<string?>();

	private Optional<long> CreateOption_MinimumEnactingDuration = new Optional<long>();

	private Optional<DescriptorFactsEntry> CreateOption_Actions = new Optional<DescriptorFactsEntry>();

    private Optional<DescriptorConditionsTreeEntry> CreateOption_Conditions = new Optional<DescriptorConditionsTreeEntry>();


	[Parameter, EditorRequired]
	public PlanOptionSubmit OnSubmit { get; set; } = null!;



	private async Task ToggleCurrentPlanOption_UI_Async() {
        if( this.Plan is null ) {
            throw new Exception( "No plan to edit options for." );
        }

        if( this.Plan.OptionsPool.Contains(this.EditOption) ) {
            this.Plan.OptionsPool.Remove( this.EditOption );
        } else {
            this.Plan.OptionsPool.Add( this.EditOption );
        }
    }

    private async Task Submit_UI_Async() {
        
    }
}
