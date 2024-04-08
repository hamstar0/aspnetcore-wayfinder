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


	private DescriptorConditionsTreeEntry CreateConditions { get; set; } = new DescriptorConditionsTreeEntry( true );

	[Parameter]
	public DescriptorConditionsTreeEntry? EditConditions { get; set; } = null!;


	[Parameter, EditorRequired]
    public SubmitDescriptorConditions OnSubmit { get; set; } = null!;   f



    private DescriptorConditionsTreeEntry GetCurrentTree() {
        if( this.CanEdit && this.EditConditions is not null ) {
            return this.EditConditions;
        } else if( this.CanCreate ) {
            return this.CreateConditions;
        }
        throw new Exception( "No current tree defined." );
    }

	public async Task AddTreeToCurrentTree_UI_Async( DataTimelineEntry tree ) {
        if( this.Disabled ) { return; }

        DescriptorConditionsTreeEntry current = this.GetCurrentTree();

		bool found = current.Children
            .Select( e => e as DescriptorEntry )
            .Any( e => e?.Id == tree.Id );
        if( found ) { return; }

		current.Add( tree, current.IsAnd );
    }
}
