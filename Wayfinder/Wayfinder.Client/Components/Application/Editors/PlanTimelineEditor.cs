using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Data.Entries;


namespace Wayfinder.Client.Components.Application.Editors;



public partial class PlanTimelineEditor {
    //[Inject]
    //public IJSRuntime Js { get; set; } = null!;

    //[Inject]
    //public ClientDataAccess Data { get; set; } = null!;


    [Parameter]
    public PlanEntry Plan { get; set; } = new PlanEntry();
}
