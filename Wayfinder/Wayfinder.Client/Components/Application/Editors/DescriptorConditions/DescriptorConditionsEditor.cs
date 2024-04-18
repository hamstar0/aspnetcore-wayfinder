using System;
using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Data;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Utility;


namespace Wayfinder.Client.Components.Application.Editors.DescriptorConditions;


public partial class DescriptorConditionsEditor {
    public delegate Task<OverridesDefault> SubmitDescriptorConditions(
        DescriptorConditionsTreeEntry tree,
        bool isEdit
    );



    //[Inject]
    //public IJSRuntime Js { get; set; } = null!;

    //[Inject]
    //public ClientDataAccess Data { get; set; } = null!;


    [Parameter]
    public bool Disabled { get; set; } = false;

	[Parameter, EditorRequired]
    public bool CanCreate { get; set; }

	[Parameter, EditorRequired]
    public bool CanEdit { get; set; }


    private DescriptorConditionsTreeEntry? SelectedConditionTree {
        get => this.Edit;
        set => this.Edit = value;
    }


    private DescriptorConditionsTreeEntry Create_Conditions { get; set; } = new DescriptorConditionsTreeEntry( true );


	[Parameter]
	public DescriptorConditionsTreeEntry? Edit { get; set; } = null!;

    private DescriptorConditionsTreeEntry? Edit_Tree = null;

    private bool IsModified => this.Edit_Tree is not null;


    [Parameter, EditorRequired]
    public SubmitDescriptorConditions OnSubmit { get; set; } = null!;   f



    private DescriptorConditionsTreeEntry GetCurrentTree() {
        if( this.CanEdit && this.Edit is not null ) {
            return this.Edit;
        } else if( this.CanCreate ) {
            return this.Create_Conditions;
        }
        throw new Exception( "No current tree defined." );
    }

	public async Task AddTreeToCurrentTree_UI_Async( DescriptorConditionsTreeEntry tree ) {
        if( this.Disabled ) { return; }

        DescriptorConditionsTreeEntry current = this.GetCurrentTree();

		bool found = current.Children
            .Select( e => e as DescriptorEntry )
            .Any( e => e?.Id == tree.Id );
        if( found ) { return; }

		current.Add( tree, current.IsAnd );
    }
}
