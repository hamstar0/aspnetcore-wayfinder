using System;
using Microsoft.AspNetCore.Components;
using Wayfinder.Client.Data;
using Wayfinder.Shared.Data;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Libraries.BooleanTree;


namespace Wayfinder.Client.Components.Application.Editors.DescriptorConditions;


public partial class DescriptorConditionsEditor {
    //[Inject]
    //public IJSRuntime Js { get; set; } = null!;

    //[Inject]
    //public ClientDataAccess Data { get; set; } = null!;


    [Parameter]
    public bool Disabled { get; set; } = false;

    [Parameter, EditorRequired]
    public Func<DescriptorConditionsTree, Task> OnEditSubmit { get; set; } = null!;

    [Parameter, EditorRequired]
    public DescriptorConditionsTree Conditions { get; set; } = null!;

    private DescriptorConditionsTree? SelectedConditionTree;



    public async Task AddScheduleToCurrentTree_UI_Async( ScheduleEntry entry ) {
        if( this.Disabled ) { return; }
        if( this.SelectedConditionTree is null ) { return; }

        bool found = this.SelectedConditionTree.Children
            .Select( e => e as DescriptorEntry )
            .Any( e => e?.Id == entry.Id );
        if( found ) { return; }

        this.SelectedConditionTree.Add( entry, this.SelectedConditionTree.IsAnd );
    }
}
