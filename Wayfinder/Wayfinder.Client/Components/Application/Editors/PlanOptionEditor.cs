﻿using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Data.Entries;


namespace Wayfinder.Client.Components.Application.Editors;



public partial class PlanOptionEditor {
    //[Inject]
    //public IJSRuntime Js { get; set; } = null!;

    //[Inject]
    //public ClientDataAccess Data { get; set; } = null!;


    [Parameter, EditorRequired]
    public PlanEntry Plan { get; set; } = null!;


	[Parameter, EditorRequired]
    public bool CanCreate { get; set; }

	[Parameter, EditorRequired]
	public PlanOptionEntry EditOption { get; set; } = null!;


    [Parameter, EditorRequired]
    public bool CanEdit { get; set; }

    [Parameter]
    public bool SubmitOnEditOnly { get; set; } = false;



	private async Task ToggleCurrentPlanOption_UI_Async() {
        f
	}
}
