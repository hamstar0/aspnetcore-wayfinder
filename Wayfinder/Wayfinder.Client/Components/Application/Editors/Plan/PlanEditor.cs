using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Data.Entries;
using Wayfinder.Shared.Utility;
using Wayfinder.Client.Data;
using Wayfinder.Shared.Data;


namespace Wayfinder.Client.Components.Application.Editors.Plan;



public partial class PlanEditor {
	public delegate Task<OverridesDefault> OnSubmitPlan( PlanEntry planEntry, bool isEdit );


	//[Inject]
	//public IJSRuntime Js { get; set; } = null!;

	[Inject]
    public ClientDataAccess Data { get; set; } = null!;


    [Parameter, EditorRequired]
    public bool CanCreate { get; set; }

    [Parameter, EditorRequired]
    public bool CanEdit { get; set; }

    [Parameter]
    public bool SubmitOnEditOnly { get; set; } = false;


	[Parameter, EditorRequired]
	public GoalEntry Goal { get; set; } = null!;


	[Parameter]
    public PlanEntry? EditPlan { get; set; } = null;

	private PlanEntry CreatePlan = new PlanEntry();


	[Parameter]
	public OnSubmitPlan? OnSubmit { get; set; } = null;



	public PlanEntry GetCurrentPlan( out bool isEdit ) {
		isEdit = this.CanEdit && this.EditPlan is not null;

		if( isEdit ) {
			return this.EditPlan!;
        }
        if( this.CanCreate ) {
            return this.CreatePlan;
        }
        throw new Exception( "No current Plan selected." );
    }


	public async Task EditPlanTimeline_UI_Async( TimelineEntry<PlanOptionEntry> timeline ) {
        if( !this.SubmitOnEditOnly ) {
            return;
        }

        PlanEntry plan = this.GetCurrentPlan( out _ );

        plan.OptionTimeline = timeline;

        await this.Submit_Async();
	}


	public async Task EditPlanOption_UI_Async( PlanOptionEntry option ) {
		if( !this.SubmitOnEditOnly ) {
			return;
		}

        PlanEntry plan = this.GetCurrentPlan( out _ );

		plan.OptionsPool.Add( option );

        await this.Submit_Async();
	}

	public async Task Submit_UI_Async() {
        await this.Submit_Async();
    }


	private async Task Submit_Async() {
		PlanEntry plan = this.GetCurrentPlan( out bool isEdit );

		if( this.OnSubmit is not null && await this.OnSubmit(plan, isEdit) ) {
			return;
		}

		if( isEdit ) {
            await this.Data.EditPlan_Async( new ClientDataAccess.EditPlanParams(
                new Optional<string?>( plan.Name ),
				new Optional<ISet<PlanOptionEntry>>( plan.OptionsPool ),
				new Optional<TimelineEntry<PlanOptionEntry>>( plan.OptionTimeline )
            ) );
        } else {
            await this.Data.CreatePlan_Async( new ClientDataAccess.CreatePlanParams(
				plan.Name,
				plan.Goal,
				plan.OptionsPool,
				plan.OptionTimeline
            ) );
        }
    }
}
