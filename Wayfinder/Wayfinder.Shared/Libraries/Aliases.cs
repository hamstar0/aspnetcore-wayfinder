﻿using System;


namespace Wayfinder.Shared.Libraries;



public record struct OverridesDefault( bool Value ) {
	public static implicit operator bool( OverridesDefault b ) => b.Value;
	public static implicit operator OverridesDefault( bool b ) => new( b );
}
