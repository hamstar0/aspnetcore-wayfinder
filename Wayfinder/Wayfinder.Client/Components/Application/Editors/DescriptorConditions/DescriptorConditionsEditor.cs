using System;
using Microsoft.AspNetCore.Components;
using Wayfinder.Client.Data;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Libraries.BooleanTree;


namespace Wayfinder.Client.Components.Application.Editors.DescriptorConditions;


public partial class DescriptorConditionsEditor
{
    //[Inject]
    //public IJSRuntime Js { get; set; } = null!;

    //[Inject]
    //public ClientDataAccess Data { get; set; } = null!;


    [Parameter]
    public bool Disabled { get; set; } = false;

    [Parameter, EditorRequired]
    public Func<BooleanTree<DescriptorEntry>, Task> OnEditSubmit { get; set; } = null!;

    [Parameter]
    public BooleanTree<DescriptorEntry> CurrentConditions { get; set; }
            = new BooleanTree<DescriptorEntry>(true);

    private BooleanTree<DescriptorEntry>? SelectedConditionTree;



    public async Task AddDescriptorToCurrentTree_UI_Async( DescriptorEntry entry ) {
        if( this.Disabled ) { return; }
        if( this.SelectedConditionTree is null ) { return; }

        bool found = this.SelectedConditionTree.Children
            .Select( e => e as DescriptorEntry )
            .Any( e => e?.Id == entry.Id );
        if( found ) { return; }

        this.SelectedConditionTree.Add( entry, this.SelectedConditionTree.IsAnd );
    }
}
