using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Libraries.BooleanTree;


namespace Wayfinder.Client.Components.Application.Editors.DescriptorConditions;



public partial class DescriptorConditionsTreeEditor
{
    //[Inject]
    //public IJSRuntime Js { get; set; } = null!;

    //[Inject]
    //public ClientDataAccess Data { get; set; } = null!;


    [Parameter]
    public bool Disabled { get; set; } = false;

    [Parameter, EditorRequired]
    public BooleanTree<DescriptorEntry> Tree { get; set; } = null!;

    [Parameter, EditorRequired]
    public Action<BooleanTree<DescriptorEntry>> OnSelectTree { get; set; } = null!;

    [Parameter]
    public string? Style { get; set; } = null;



    public async Task AddTree_UI_Async(bool isAnd)
    {
        if (Disabled) { return; }

        var subTree = (IBoolean)new BooleanTree<DescriptorEntry>(isAnd);

        Tree.Add(subTree, isAnd);
    }
}
