using System;
using Microsoft.AspNetCore.Components;
using Wayfinder.Client.Data;
using Wayfinder.Shared.Data.Entries;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Data.Schedule;
using Wayfinder.Shared.Libraries.BooleanTree;


namespace Wayfinder.Client.Components.Application.Editors.Descriptor;


public partial class DescriptorEditor {
    //[Inject]
    //public IJSRuntime Js { get; set; } = null!;

    [Inject]
    public ClientDataAccess Data { get; set; } = null!;


    private IEnumerable<DescriptorEntry> SearchOptions = new List<DescriptorEntry>();

    private TermEntry? TermSubj;
    private TermEntry? TermRel;

    private ScheduleEntry? Schedule = null;

    private BooleanTree<DescriptorEntry> Conditions = new BooleanTree<DescriptorEntry>( true );

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
    public Func<ClientDataAccess.EditDescriptorParams, Task<bool>>? OnDescriptorEdit { get; set; } = null;

    [Parameter]
    public Func<ClientDataAccess.CreateDescriptorParams, Task<DescriptorEntry?>>? OnDescriptorCreate { get; set; } = null;

    [Parameter, EditorRequired]
    public Func<DescriptorEntry, Task> OnDescriptorSelect { get; set; } = null!;



    private async Task SetTermA_UI_Async( TermEntry term ) {
        if( this.Disabled ) { return; }

        bool isNew = !this.TermSubj?.Equals( term ) ?? true;

        this.TermSubj = term;

        if( isNew ) {
            await this.OnEdit_Async();
        }
    }

    private async Task SetTermB_UI_Async( TermEntry term ) {
        if( this.Disabled ) { return; }

        bool isNew = !this.TermRel?.Equals( term ) ?? true;
        this.TermRel = term;

        if( isNew ) {
            await this.OnEdit_Async();
        }
    }

    private async Task<bool> SetSchedule_UI_Async( ScheduleEntry sched ) {
        if( this.Disabled ) {
            return true;
        }

        bool isChanged = this.Schedule is null
            ? true
            : !this.Schedule.Equals( sched );

        this.Schedule = sched;

        if( isChanged ) {
            await this.OnEdit_Async();
        }

        return true;    // false posts changes to server
    }

    private async Task SetConditions_UI_Async( BooleanTree<DescriptorEntry> conditions ) {
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
