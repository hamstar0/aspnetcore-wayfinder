using System;
using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Utility;
using Wayfinder.Shared.Data;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Client.Data;
using Wayfinder.Shared.Utility.Timeline.Data;


namespace Wayfinder.Client.Components.Application.Editors.Descriptor;



public partial class DescriptorEditor {
	public delegate Task<OverridesDefault> DescriptorEdit( ClientDataAccess.EditDescriptorParams parameters );

	public delegate Task<DescriptorEntry> DescriptorCreate( ClientDataAccess.CreateDescriptorParams parameters );


	//[Inject]
	//public IJSRuntime Js { get; set; } = null!;

	[Inject]
    public ClientDataAccess Data { get; set; } = null!;


    private bool IsWriteModeEdit = false;


    private IEnumerable<DescriptorEntry> SearchResults = new List<DescriptorEntry>();

    private DescriptorFactsEntry Search_Facts => this.Create_Facts;

    private DescriptorConditionsTreeEntry Search_Conditions => this.Create_Conditions;


    private DescriptorFactsEntry Create_Facts = new( new List<TimelineEventEntry<TimelineDataEntry>>() );

    private DescriptorConditionsTreeEntry Create_Conditions = new( true );


    private DescriptorEntry? SelectedDescriptor {
        get => this.Edit;
        set => this.Edit = value;
    }


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
    public DescriptorEntry? Edit { get; set; } = null!;

    private DescriptorFactsEntry? EditChanged_Facts = null;

    private DescriptorConditionsTreeEntry? EditChanged_Conditions = null;

    private bool IsModified => this.EditChanged_Facts is not null || this.EditChanged_Conditions is not null;


    [Parameter]
    public DescriptorEdit? OnDescriptorEdit { get; set; } = null;

	[Parameter]
    public DescriptorCreate? OnDescriptorCreate { get; set; } = null;

    [Parameter, EditorRequired]
    public Func<DescriptorEntry, Task> OnDescriptorSelect { get; set; } = null!;



    protected override void OnParametersSet() {
        base.OnParametersSet();

        this.IsWriteModeEdit = this.CanEdit && this.Edit is not null;
    }


    public DescriptorFactsEntry GetCurrentFacts() {
        if( this.IsWriteModeEdit ) {
            if( this.Edit is null ) {
                throw new NullReferenceException();
            }
            return this.Edit.Facts;
        }

        return this.Create_Facts;
    }
    
    public DescriptorConditionsTreeEntry GetCurrentConditions() {
        if( this.IsWriteModeEdit ) {
            if( this.Edit is null ) {
                throw new NullReferenceException();
            }
            return this.Edit.Conditions;
        }

        return this.Create_Conditions;
    }


    private async Task SetFacts_Async( DescriptorFactsEntry facts ) {
        if( this.IsWriteModeEdit ) {
            this.EditChanged_Facts = facts;

            await this.OnEdit_Async();
        } else {
            this.Create_Facts = facts;
        }
    }

    private async Task<bool> SetFacts_UI_Async( DescriptorFactsEntry facts ) {
        if( this.Disabled ) {
            return true;
        }

        await this.SetFacts_Async( facts );

        return true;    // false posts changes to server
    }


    private async Task SetConditions_Async( DescriptorConditionsTreeEntry conditions ) {
        if( this.IsWriteModeEdit ) {
            this.EditChanged_Conditions = conditions;

            await this.OnEdit_Async();
        } else {
            this.Create_Conditions = conditions;
        }
    }

    private async Task SetConditions_UI_Async( DescriptorConditionsTreeEntry conditions ) {
        if( this.Disabled ) { return; }

        await this.SetConditions_Async( conditions );
    }


    private async Task OnEdit_Async() {
        if( this.SubmitOnEditOnly ) {
            if( this.CanEditDescriptor() ) {
                await this.SubmitEdit_Async();
            }
            //else if( this.CanCreateDescriptor() ) {
            //    await this.SubmitCreate_Async();
            //}
        }

        await this.UpdateDescriptorSearchResults_Async();
    }
}
