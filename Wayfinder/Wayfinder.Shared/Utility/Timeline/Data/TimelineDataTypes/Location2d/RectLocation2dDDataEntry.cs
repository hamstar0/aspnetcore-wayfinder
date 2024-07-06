using System;
using Wayfinder.Shared.Utility.Timeline.Data;


namespace Wayfinder.Shared.Utility.Timeline.Data.TimelineDataTypes.Location2d;



public class RectLocation2dDDataEntry : Location2dDDataEntry,
			IEquatable<RectLocation2dDDataEntry> {
	public double LatitudeN;

	public double LongitudeW;

	public double LatitudeS;

	public double LongitudeE;



	public RectLocation2dDDataEntry( double latitudeN, double longitudeW, double latitudeS, double longitudeE ) {
		LatitudeN = latitudeN;
		LongitudeW = longitudeW;
		LatitudeS = latitudeS;
		LongitudeE = longitudeE;
	}

	public bool Equals( RectLocation2dDDataEntry? other ) {
		if( other is null ) { return false; }
		//if( this.For != other.For ) { return false; }
		if( LatitudeN != other.LatitudeN ) { return false; }
		if( LongitudeW != other.LongitudeW ) { return false; }
		if( LatitudeS != other.LatitudeS ) { return false; }
		if( LongitudeE != other.LongitudeE ) { return false; }
		return true;
	}

	protected override bool ValidateOtherWithSelf( TimelineDataEntry rawValidator ) {
		throw new NotImplementedException();
	}
}

