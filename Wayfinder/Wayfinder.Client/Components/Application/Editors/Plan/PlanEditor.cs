using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Utility;
using Wayfinder.Client.Data;
using Wayfinder.Shared.Data;
using Wayfinder.Shared.DataEntries;


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


	public bool IsModified => this.Edit_Name.HasValue || this.Edit_OptionsPool.HasValue;

	[Parameter, EditorRequired]
	public GoalEntry Goal { get; set; } = null!;


	private string? Create_Name = null;

    private ISet<PlanOptionEntry> Create_OptionsPool = new HashSet<PlanOptionEntry>();

    private TimelineEntry<PlanOptionEntry> Create_OptionTimeline = new TimelineEntry<PlanOptionEntry>(

	);


	[Parameter]
    public PlanEntry? Edit { get; set; } = null;

    public Optional<string?> Create_Name = new Optional<string?>();

    public Optional<ISet<PlanOptionEntry>> Create_OptionsPool = new Optional<ISet<PlanOptionEntry>>();

    public Optional<TimelineEntry<PlanOptionEntry>> Create_OptionTimeline = new Optional<TimelineEntry<PlanOptionEntry>>();


    [Parameter]
	public OnSubmitPlan? OnSubmit { get; set; } = null;



	public ISet<PlanOptionEntry> GetCurrentOptionPool( out bool isEdit ) {
		isEdit = this.CanEdit && this.Edit is not null;

		if( isEdit ) {
			return this.Edit!.OptionsPool;
		} else if( this.CanCreate ) {
			return this.Create_OptionsPool;
		} else {
			throw new NotImplementedException();
		}
	}

	public TimelineEntry<PlanOptionEntry> GetCurrentOptionTimeline( out bool isEdit ) {
		isEdit = this.CanEdit && this.Edit is not null;

		if( isEdit ) {
			return this.Edit!.OptionTimeline;
		} else if( this.CanCreate ) {
			return this.Create_OptionTimeline;
		} else {
			throw new NotImplementedException();
		}
	}


	public async Task EditPlanTimeline_UI_Async( TimelineEntry<PlanOptionEntry> timeline ) {
        if( !this.SubmitOnEditOnly ) {
            return;
        }

		if( this.CanEdit && this.Edit is not null ) {
			this.Edit.OptionTimeline = timeline;
		} else if( this.CanCreate ) {
			this.Create_OptionTimeline = timeline;
		} else {
			throw new NotImplementedException();
		}

        await this.Submit_Async();
	}

	public async Task EditPlanOption_UI_Async( PlanOptionEntry option ) {
		if( !this.SubmitOnEditOnly ) {
			return;
		}

		if( this.CanEdit && this.Edit is not null ) {
			this.Edit.OptionsPool.Add( option );
		} else if( this.CanCreate ) {
			this.Create_OptionsPool.Add( option );
		} else {
			throw new NotImplementedException();
		}

        await this.Submit_Async();
	}

	public async Task EditPlanOptionPool_UI_Async( ISet<PlanOptionEntry> optionPool ) {
		if( !this.SubmitOnEditOnly ) {
			return;
		}

		if( this.CanEdit && this.Edit is not null ) {
			this.Edit.OptionsPool = optionPool;
		} else if( this.CanCreate ) {
			this.Create_OptionsPool = optionPool;
		} else {
			throw new NotImplementedException();
		}

        await this.Submit_Async();
	}


	public async Task Submit_UI_Async() {
        await this.Submit_Async();
    }


	private async Task Submit_Async() {
		bool isEdit = this.CanEdit && this.Edit is not null;
		bool isCreate = this.CanCreate;

		PlanEntry plan = isEdit ? this.Edit!
			: isCreate ? new PlanEntry(
				this.Create_Name,
				this.Goal,
				this.Create_OptionsPool,
				this.Create_OptionTimeline
			)
			: throw new NotImplementedException();
		if( isCreate ) {
			plan.Name = this.Create_Name;
			plan.OptionsPool = this.Create_OptionsPool;
			plan.OptionTimeline = this.Create_OptionTimeline;

			if( this.CanEdit ) {
				this.Edit = plan;
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
