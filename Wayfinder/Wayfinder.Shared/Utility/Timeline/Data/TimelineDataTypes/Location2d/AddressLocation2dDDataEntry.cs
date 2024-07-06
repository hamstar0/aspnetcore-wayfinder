using System;
using Wayfinder.Shared.Utility.Timeline.Data;


namespace Wayfinder.Shared.Utility.Timeline.Data.TimelineDataTypes.Location2d;



public class AddressLocation2dDDataEntry : Location2dDDataEntry,
			IEquatable<AddressLocation2dDDataEntry> {
	public string Address;



	public AddressLocation2dDDataEntry( string address ) {
		Address = address;
	}


	public bool Equals( AddressLocation2dDDataEntry? other ) {
		if( other is null ) { return false; }
		//if( this.For != other.For ) { return false; }
		if( Address != other.Address ) { return false; }
		return true;
	}

	protected override bool ValidateOtherWithSelf( TimelineDataEntry rawValidator ) {
		throw new NotImplementedException();
	}
}

