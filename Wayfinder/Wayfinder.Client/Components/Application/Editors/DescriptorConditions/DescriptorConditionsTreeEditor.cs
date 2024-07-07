using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Data;
using Wayfinder.Shared.DataEntries.Descriptor;
using Wayfinder.Shared.Utility.DataStructures;


namespace Wayfinder.Client.Components.Application.Editors.DescriptorConditions;



public partial class DescriptorConditionsTreeEditor {
    //[Inject]
    //public IJSRuntime Js { get; set; } = null!;

    //[Inject]
    //public ClientDataAccess Data { get; set; } = null!;


    [Parameter]
    public bool Disabled { get; set; } = false;

    [Parameter, EditorRequired]
    public DescriptorConditionsTreeEntry Conditions { get; set; } = null!;

    [Parameter, EditorRequired]
    public Action<DescriptorConditionsTreeEntry> OnTreeSelect { get; set; } = null!;

    [Parameter]
    public string? Style { get; set; } = null;



    public async Task AddTree_UI_Async( bool isAnd ) {
        if (Disabled) { return; }

        var subTree = (IBoolean<DescriptorConditionBooleanContext>)new DescriptorConditionsTreeEntry( isAnd );

        this.Conditions.Add( subTree, isAnd );
    }
}
