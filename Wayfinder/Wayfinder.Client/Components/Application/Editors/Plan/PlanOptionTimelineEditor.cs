using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Data.Entries;
using Wayfinder.Shared.Utility;


namespace Wayfinder.Client.Components.Application.Editors.Plan;



public partial class PlanOptionTimelineEditor {
	public delegate Task<OverridesDefault> PlanOptionTimelineSubmit(
        TimelineEntry<PlanOptionEntry> timeline,
        bool isEdit
    );


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


    [Parameter, EditorRequired]
    public TimelineEntry<PlanOptionEntry> EditTimeline { get; set; } = null!;

    private TimelineEntry<PlanOptionEntry> CreateTimeline = new TimelineEntry<PlanOptionEntry>();


    [Parameter, EditorRequired]
    public PlanOptionTimelineSubmit OnSubmit {f get; set; } = null!;



    private void GetPlanTimeline() {
    }

    private async Task OnMouseDown_UI_Async() {
        f
    }

	private async Task OnMouseUp_UI_Async() {
        f
    }
}
