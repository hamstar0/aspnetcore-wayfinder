using System;
using Wayfinder.Shared.Utility.Timeline.Data;


namespace Wayfinder.Shared.Utility.Timeline.Data.TimelineDataTypes.Location2d;



public class RangeLocation2dDDataEntry : Location2dDDataEntry,
			IEquatable<RangeLocation2dDDataEntry> {
	public double Longitude;

	public double Latitude;

	public double Radius;



	public RangeLocation2dDDataEntry( double longitude, double latitude, double radius ) {
		Longitude = longitude;
		Latitude = latitude;
		Radius = radius;
	}


	public bool Equals( RangeLocation2dDDataEntry? other ) {
		if( other is null ) { return false; }
		//if( this.For != other.For ) { return false; }
		if( Longitude != other.Longitude ) { return false; }
		if( Latitude != other.Latitude ) { return false; }
		if( Radius != other.Radius ) { return false; }
		return true;
	}

	protected override bool ValidateOtherWithSelf( TimelineDataEntry rawValidator ) {
		throw new NotImplementedException();
	}
}

