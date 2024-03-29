using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Libraries;
using Wayfinder.Shared.Data.Entries;


namespace Wayfinder.Client.Components.Application.Editors.Plan;



public partial class PlanOptionTreeEditor {
    public delegate Task<OverridesDefault> PlanOptionSubmit( PlanOptionEntry option, bool isEdit );


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
    public PlanEntry Plan { get; set; } = null!;


	[Parameter, EditorRequired]
	public PlanOptionSubmit OnSubmit { get; set; } = null!; unused?
}
