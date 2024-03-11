using System.ComponentModel.DataAnnotations;


namespace Wayfinder.Shared.Data.Entries.Descriptor.DescriptorDataTypes;



public class ScalarRangeDDataEntry : DescriptorDataEntry, IEquatable<ScalarRangeDDataEntry> {
    public double MinValue { get; private set; }
    public double MaxValue { get; private set; }



    public ScalarRangeDDataEntry( double minValue, double maxValue )
                : base( DescriptorDataType.ScalarRange ) {
        if( minValue > maxValue ) {
            throw new ArgumentException();
        }

        MinValue = minValue;
        MaxValue = maxValue;
    }

    public bool Equals( ScalarRangeDDataEntry? other ) {
        if( other is null ) { return false; }
        //if( this.For != other.For ) { return false; }
        if( this.MinValue != other.MinValue ) { return false; }
        if( this.MaxValue != other.MaxValue ) { return false; }
        return true;
    }


    protected override bool ValidateOtherWithSelf( DescriptorDataEntry rawValidator ) {
        var validator = (ScalarRangeDDataEntry)rawValidator;

        if( MinValue == validator.MinValue && MaxValue == validator.MaxValue ) {
            return true;//0;
        }
        if( MinValue <= validator.MinValue && MaxValue >= validator.MaxValue ) {
            return true;//1;
        }
        if( MinValue > validator.MinValue && MaxValue > validator.MaxValue ) {
            return true;//0;
        }
        if( MinValue < validator.MinValue && MaxValue < validator.MaxValue ) {
            return true;//0;
        }
        return false;//-1;
    }
}
