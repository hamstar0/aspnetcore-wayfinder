using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Utility;


namespace Wayfinder.Client.Components.Standard.BooleanTreeEditor;


public interface IBooleanEditComponent<T> : IComponent {
	IBoolean<T> Data { get; set; }
}

