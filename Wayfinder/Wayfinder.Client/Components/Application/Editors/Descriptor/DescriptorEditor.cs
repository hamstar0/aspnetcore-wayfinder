using System;
using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Libraries;
using Wayfinder.Shared.Data;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Client.Data;


namespace Wayfinder.Client.Components.Application.Editors.Descriptor;



public partial class DescriptorEditor {
	public delegate Task<OverridesDefault> DescriptorEdit( ClientDataAccess.EditDescriptorParams parameters );

	public delegate Task<DescriptorEntry> DescriptorCreate( ClientDataAccess.CreateDescriptorParams parameters );


	//[Inject]
	//public IJSRuntime Js { get; set; } = null!;

	[Inject]
    public ClientDataAccess Data { get; set; } = null!;


    private IEnumerable<DescriptorEntry> SearchOptions = new List<DescriptorEntry>();

    private DataTimelineEntry Facts = new DataTimelineEntry();

	private DescriptorConditionsTree Conditions = new DescriptorConditionsTree( true );

	private DescriptorEntry? SelectedDescriptor = null;


    private bool IsModified = false;


    [Parameter]
    public bool Disabled { get; set; } = false;

    [Parameter]
    public bool Abridged { get; set; } = false;


    [Parameter]
    public string? Label { get; set; }


    [Parameter, EditorRequired]
    public bool CanCreate { get; set; }
    
    [Parameter, EditorRequired]
    public bool CanEdit { get; set; }

    [Parameter, EditorRequired]
    public bool CanSearch { get; set; }

    [Parameter]
    public bool SubmitOnEditOnly { get; set; } = false;


    [Parameter]
    public DescriptorEdit? OnDescriptorEdit { get; set; } = null;

	[Parameter]
    public DescriptorCreate? OnDescriptorCreate { get; set; } = null;

    [Parameter, EditorRequired]
    public Func<DescriptorEntry, Task> OnDescriptorSelect { get; set; } = null!;



    private async Task<bool> SetFacts_UI_Async( DataTimelineEntry sched ) {
        if( this.Disabled ) {
            return true;
        }

        bool isChanged = this.Facts is null
            ? true
            : !this.Facts.Equals( sched );

        this.Facts = sched;

        if( isChanged ) {
            await this.OnEdit_Async();
        }

        return true;    // false posts changes to server
    }

    private async Task SetConditions_UI_Async( DescriptorConditionsTree conditions ) {
        if( this.Disabled ) { return; }

        bool isChanged = this.Conditions is null
            ? true
            : !this.Conditions.Equals( conditions );

        if( isChanged ) {
            this.Conditions = conditions;

            await this.OnEdit_Async();
        }
    }


    private async Task OnEdit_Async() {
        this.IsModified = true;

        if( this.SubmitOnEditOnly ) {
            if( this.CanEditDescriptor() ) {
                await this.SubmitEdit_Async();
            } else if( this.CanCreateDescriptor() ) {
                await this.SubmitCreate_Async();
            }
        }

        await this.UpdateDescriptorSearchResults_Async();
    }
}
