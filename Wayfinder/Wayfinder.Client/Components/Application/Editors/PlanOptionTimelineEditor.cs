using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Data.Entries;
using Wayfinder.Shared.Libraries;


namespace Wayfinder.Client.Components.Application.Editors;



public partial class PlanOptionTimelineEditor {
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


	OnSubmit
}
