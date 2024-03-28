using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Data.Entries;
using Wayfinder.Shared.Libraries;
using Wayfinder.Client.Data;
using Wayfinder.Shared.Data;
using Wayfinder.Shared.Data.Entries.Descriptor;


namespace Wayfinder.Client.Components.Application.Editors;



public partial class PlanEditor {
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


    public Timeline<PlanOptionEntry> GetCurrentPlanTimeline() {
        return this.GetCurrentPlan().OptionTimeline;
    }


	public async Task EditPlanTimeline_UI_Async( DataTimelineEntry timeline ) {
        if( !this.SubmitOnEditOnly ) {
            return;
        }

        f
    }


	public async Task EditPlanOption_UI_Async( DescriptorEntry optionFor, PlanOptionEntry option ) {
		if( !this.SubmitOnEditOnly ) {
			return;
		}

		f
	}

	public async Task Submit_UI_Async() {
        if( this.CanEdit && this.EditPlan is not null ) {
            await this.Data.EditPlan_Async( new ClientDataAccess.EditPlanParams(
                new Optional<string?>( this.EditPlan ),
                this.CreatePlan.Goal,
                this.CreatePlan.Options,
                this.CreatePlan.OptionTimeline
            ) );
        } else if( this.CanCreate ) {
            await this.Data.CreatePlan_Async( new ClientDataAccess.CreatePlanParams(
                this.CreatePlan.Name,
                this.CreatePlan.Goal,
                this.CreatePlan.Options,
                this.CreatePlan.OptionTimeline
            ) );
        }
        throw new Exception( "No submit option available." );
    }
}
