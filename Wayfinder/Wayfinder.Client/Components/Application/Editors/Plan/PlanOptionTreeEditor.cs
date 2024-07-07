using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Utility;
using Wayfinder.Shared.DataEntries.Descriptor;
using Wayfinder.Shared.DataEntries;


namespace Wayfinder.Client.Components.Application.Editors.Plan;



public partial class PlanOptionTreeEditor {
    public delegate Task PlanOptionsSinglePoolEditSubmit( PlanOptionEntry option, bool isAdded );


    //[Inject]
    //public IJSRuntime Js { get; set; } = null!;

    //[Inject]
    //public ClientDataAccess Data { get; set; } = null!;


    [Parameter]
    public bool SubmitOnEditOnly { get; set; } = false;


	[Parameter, EditorRequired]
	public GoalEntry Goal { get; set; } = null!;

	[Parameter, EditorRequired]
	public ISet<PlanOptionEntry> EditOptionsPool { get; set; } = null!;


	[Parameter, EditorRequired]
	public PlanOptionsSinglePoolEditSubmit OnSubmit { get; set; } = null!;



	private IEnumerable<PlanOptionEntry> GetOptionsForCondition( DescriptorCondition condition ) {
		var options = new HashSet<PlanOptionEntry>();

		foreach( PlanOptionEntry option in this.EditOptionsPool ) {
			if( conditions.Evaluate(option.Conditions) ) {
				options.Add( option );
			}
		}

		return options;
	}

	private async Task SubmitOptionToggleWithinPool_UI_Async( PlanOptionEntry option ) {
		bool isAdded = this.EditOptionsPool.Add( option );
		if( !isAdded ) {
			this.EditOptionsPool.Remove( option );
		}

		await this.OnSubmit( option, isAdded );
	}
}
