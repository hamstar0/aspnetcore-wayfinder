using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Data.Entries;


namespace Wayfinder.Client.Components.Application.Editors;



public partial class PlanOptionEditor {
    //[Inject]
    //public IJSRuntime Js { get; set; } = null!;

    //[Inject]
    //public ClientDataAccess Data { get; set; } = null!;


    [Parameter, EditorRequired]
    public PlanOptionEntry Option { get; set; } = null!;
}
