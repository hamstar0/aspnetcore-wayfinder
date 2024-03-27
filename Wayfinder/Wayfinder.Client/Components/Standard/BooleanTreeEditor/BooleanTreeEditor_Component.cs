using Microsoft.AspNetCore.Components;
using Wayfinder.Shared.Libraries;


namespace Wayfinder.Client.Components.Standard.BooleanTreeEditor;


public interface IBooleanEditComponent<T> : IComponent {
	IBoolean<T> Data { get; set; }
}

