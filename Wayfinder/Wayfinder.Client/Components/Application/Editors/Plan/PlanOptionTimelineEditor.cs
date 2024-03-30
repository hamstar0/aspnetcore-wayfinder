using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Data.Entries;
using Wayfinder.Shared.Utility;


namespace Wayfinder.Client.Components.Application.Editors.Plan;



public partial class PlanOptionTimelineEditor {
	public delegate Task<OverridesDefault> PlanOptionTimelineSubmit(
        Timeline<PlanOptionEntry> timeline,
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
    public Timeline<PlanOptionEntry> EditTimeline { get; set; } = null!;

    private Timeline<PlanOptionEntry> CreateTimeline = new Timeline<PlanOptionEntry>();


    [Parameter, EditorRequired]
    public PlanOptionTimelineSubmit OnSubmit { get; set; } = null!;
}
