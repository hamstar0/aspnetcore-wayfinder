using System;


namespace Wayfinder.Shared.Data.Entries.Descriptor.DescriptorDataTypes.Location2d;



public class RectLocation2dDDataEntry : Location2dDDataEntry,
            IEquatable<RectLocation2dDDataEntry> {
    public double LatitudeN;

    public double LongitudeW;

    public double LatitudeS;

    public double LongitudeE;



    public RectLocation2dDDataEntry( double latitudeN, double longitudeW, double latitudeS, double longitudeE ) {
        this.LatitudeN = latitudeN;
        this.LongitudeW = longitudeW;
        this.LatitudeS = latitudeS;
        this.LongitudeE = longitudeE;
    }

    public bool Equals( RectLocation2dDDataEntry? other ) {
        if( other is null ) { return false; }
        //if( this.For != other.For ) { return false; }
        if( this.LatitudeN != other.LatitudeN ) { return false; }
        if( this.LongitudeW != other.LongitudeW ) { return false; }
        if( this.LatitudeS != other.LatitudeS ) { return false; }
        if( this.LongitudeE != other.LongitudeE ) { return false; }
        return true;
    }

    protected override bool ValidateOtherWithSelf( DescriptorDataEntry rawValidator ) {
        throw new NotImplementedException();
    }
}

