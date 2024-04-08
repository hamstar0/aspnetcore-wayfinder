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

	public Optional<string?> CreatePlan_Name = new Optional<string?>();

	public Optional<ISet<PlanOptionEntry>> CreatePlan_OptionsPool = new Optional<ISet<PlanOptionEntry>>();

	public Optional<TimelineEntry<PlanOptionEntry>> CreatePlan_OptionTimeline = new Optional<TimelineEntry<PlanOptionEntry>>();


	[Parameter]
	public OnSubmitPlan? OnSubmit { get; set; } = null;



	public ISet<PlanOptionEntry> GetCurrentOptionPool( out bool isEdit ) {
		isEdit = this.CanEdit && this.EditPlan is not null;

		if( isEdit ) {
			return this.EditPlan!.OptionsPool;
		} else if( this.CanCreate ) {
			return this.CreatePlan_OptionsPool;
		} else {
			throw new NotImplementedException();
		}
	}

	public TimelineEntry<PlanOptionEntry> GetCurrentOptionTimeline( out bool isEdit ) {
		isEdit = this.CanEdit && this.EditPlan is not null;

		if( isEdit ) {
			return this.EditPlan!.OptionTimeline;
		} else if( this.CanCreate ) {
			return this.CreatePlan_OptionTimeline;
		} else {
			throw new NotImplementedException();
		}
	}


	public async Task EditPlanTimeline_UI_Async( TimelineEntry<PlanOptionEntry> timeline ) {
        if( !this.SubmitOnEditOnly ) {
            return;
        }

		if( this.CanEdit && this.EditPlan is not null ) {
			this.EditPlan.OptionTimeline = timeline;
		} else if( this.CanCreate ) {
			this.CreatePlan_OptionTimeline = timeline;
		} else {
			throw new NotImplementedException();
		}

        await this.Submit_Async();
	}

	public async Task EditPlanOption_UI_Async( PlanOptionEntry option ) {
		if( !this.SubmitOnEditOnly ) {
			return;
		}

		if( this.CanEdit && this.EditPlan is not null ) {
			this.EditPlan.OptionsPool.Add( option );
		} else if( this.CanCreate ) {
			this.CreatePlan_OptionsPool.Add( option );
		} else {
			throw new NotImplementedException();
		}

        await this.Submit_Async();
	}

	public async Task EditPlanOptionPool_UI_Async( ISet<PlanOptionEntry> optionPool ) {
		if( !this.SubmitOnEditOnly ) {
			return;
		}

		if( this.CanEdit && this.EditPlan is not null ) {
			this.EditPlan.OptionsPool = optionPool;
		} else if( this.CanCreate ) {
			this.CreatePlan_OptionsPool = optionPool;
		} else {
			throw new NotImplementedException();
		}

        await this.Submit_Async();
	}


	public async Task Submit_UI_Async() {
        await this.Submit_Async();
    }


	private async Task Submit_Async() {
		bool isEdit = this.CanEdit && this.EditPlan is not null;
		bool isCreate = this.CanCreate;

		PlanEntry plan = isEdit ? this.EditPlan!
			: isCreate ? new PlanEntry(
				this.CreatePlan_Name,
				this.Goal,
				this.CreatePlan_OptionsPool,
				this.CreatePlan_OptionTimeline
			)
			: throw new NotImplementedException();
		if( isCreate ) {
			plan.Name = this.CreatePlan_Name;
			plan.OptionsPool = this.CreatePlan_OptionsPool;
			plan.OptionTimeline = this.CreatePlan_OptionTimeline;

			if( this.CanEdit ) {
				this.EditPlan = plan;
			}
		}

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
