using System;


namespace Wayfinder.Shared.Data.Entries.Descriptor.DescriptorDataTypes.Location2d;



public class RangeLocation2dDDataEntry : Location2dDDataEntry,
            IEquatable<RangeLocation2dDDataEntry> {
    public double Longitude;

    public double Latitude;

    public double Radius;



    public RangeLocation2dDDataEntry( double longitude, double latitude, double radius ) {
        this.Longitude = longitude;
		this.Latitude = latitude;
		this.Radius = radius;
    }


    public bool Equals( RangeLocation2dDDataEntry? other ) {
        if( other is null ) { return false; }
        //if( this.For != other.For ) { return false; }
        if( this.Longitude != other.Longitude ) { return false; }
        if( this.Latitude != other.Latitude ) { return false; }
        if( this.Radius != other.Radius ) { return false; }
        return true;
    }

    protected override bool ValidateOtherWithSelf( DescriptorDataEntry rawValidator ) {
        throw new NotImplementedException();
    }
}

