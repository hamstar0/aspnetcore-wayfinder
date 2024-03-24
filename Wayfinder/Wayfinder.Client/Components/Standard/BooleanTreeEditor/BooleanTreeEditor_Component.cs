using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Libraries.BooleanTree;


namespace Wayfinder.Client.Components.Standard.BooleanTreeEditor;


public interface IBooleanEditComponent<T> : IComponent
{
    IBoolean<T> Data { get; set; }
}

