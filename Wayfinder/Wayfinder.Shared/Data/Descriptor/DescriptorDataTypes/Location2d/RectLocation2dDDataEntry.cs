using System;


namespace Wayfinder.Shared.Data.Entries.Descriptor.DescriptorDataTypes.Location2d;



public class RectLocation2dDDataEntry : Location2dDDataEntry,
            IEquatable<RectLocation2dDDataEntry> {
    public double LatitudeN { get; set; }

    public double LongitudeW { get; set; }

    public double LatitudeS { get; set; }

    public double LongitudeE { get; set; }



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

