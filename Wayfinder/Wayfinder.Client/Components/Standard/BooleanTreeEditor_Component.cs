using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Libraries.BooleanTree;


namespace Wayfinder.Client.Components.Standard;


public interface IBooleanEditComponent : IComponent {
    IBoolean Data { get; set; }
}

