using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Data.Entries;
using Wayfinder.Shared.Libraries;
using Wayfinder.Shared.Libraries.BooleanTree;


namespace Wayfinder.Client.Components.Application.Editors;



public partial class PlanOptionTreeEditor {
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
    public Tree<PlanOptionEntry> EditTree { get; set; } = null!;

    private Tree<PlanOptionEntry> CreateTree = new Tree<PlanOptionEntry>();
}
