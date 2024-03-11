using System;


namespace Wayfinder.Shared.Data.Entries.Descriptor.DescriptorDataTypes.Location2d;



public class AddressLocation2dDDataEntry : Location2dDDataEntry,
            IEquatable<AddressLocation2dDDataEntry> {
    public string Address { get; set; } = null!;



    public bool Equals( AddressLocation2dDDataEntry? other ) {
        if( other is null ) { return false; }
        //if( this.For != other.For ) { return false; }
        if( this.Address != other.Address ) { return false; }
        return true;
    }

    protected override bool ValidateOtherWithSelf( DescriptorDataEntry rawValidator ) {
        throw new NotImplementedException();
    }
}

