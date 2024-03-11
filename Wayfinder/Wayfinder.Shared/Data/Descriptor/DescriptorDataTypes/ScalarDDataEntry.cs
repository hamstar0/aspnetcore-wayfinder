using System.ComponentModel.DataAnnotations;


namespace Wayfinder.Shared.Data.Entries.Descriptor.DescriptorDataTypes;



public class ScalarDDataEntry : DescriptorDataEntry, IEquatable<ScalarDDataEntry> {
    public double Value { get; private set; }



    public ScalarDDataEntry( double value ) : base( DescriptorDataType.ScalarRange ) {
        this.Value = value;
    }

    public bool Equals( ScalarDDataEntry? other ) {
        if( other is null ) { return false; }
        //if( this.For != other.For ) { return false; }
        if( this.Value != other.Value ) { return false; }
        return true;
    }


    protected override bool ValidateOtherWithSelf( DescriptorDataEntry rawValidator ) {
        if( rawValidator is null || rawValidator is not ScalarRangeDDataEntry ) {
            throw new ArgumentException();
        }
        var validator = (ScalarRangeDDataEntry)rawValidator;

        return this.Value < validator.MinValue
            ? false
            : this.Value > validator.MaxValue
            ? false
            : true;
    }
}
