using System.Collections.Frozen;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Wayfinder.Shared.Data.Entries.Descriptor.DescriptorDataTypes;
using Wayfinder.Shared.Data.Entries.Descriptor.DescriptorDataTypes.Location2d;


namespace Wayfinder.Shared.Data.Entries.Descriptor;



public enum DescriptorDataType {
    Scalar = 1,
    ScalarRange,
    Location2d,
    Location2d_Rect,
    Location2d_Area,
    Location2d_Address
}



public abstract class DescriptorDataEntry {
    public readonly static IReadOnlyDictionary<DescriptorDataType, string> DataTypeNames
        = new ReadOnlyDictionary<DescriptorDataType, string>( new Dictionary<DescriptorDataType, string> {
            { DescriptorDataType.Scalar, "Scalar" },
            { DescriptorDataType.ScalarRange, "Scalar Range" },
            { DescriptorDataType.Location2d, "Map Location" },
            { DescriptorDataType.Location2d_Rect, "Map Location (Rectangle)" },
            { DescriptorDataType.Location2d_Area, "Map Location (Radius)" },
            { DescriptorDataType.Location2d_Address, "Map Location (Address)" },
        } );

    public readonly static IReadOnlySet<DescriptorDataType> RangeTypes
        = new List<DescriptorDataType>( new DescriptorDataType[] {
            DescriptorDataType.ScalarRange,
            DescriptorDataType.Location2d,
            DescriptorDataType.Location2d_Rect,
            DescriptorDataType.Location2d_Area,
            DescriptorDataType.Location2d_Address,
        } ).ToFrozenSet();



    public DescriptorDataType ValidatorType { get; private set; }

    //public DescriptorDataEntry? For { get; set; } = null!; // 1:1 ??????????????



    protected DescriptorDataEntry( DescriptorDataType validatorType ) {
        this.ValidatorType = validatorType;
    }


    public bool ValidateOther( DescriptorDataEntry toValidateRaw ) {
        switch( this.ValidatorType ) {
        case DescriptorDataType.Scalar:
            if( toValidateRaw.GetType() != typeof(ScalarDDataEntry) ) {
                throw new ArgumentException();
            }
            break;
        case DescriptorDataType.ScalarRange:
            if( toValidateRaw.GetType() != typeof(ScalarRangeDDataEntry) ) {
                throw new ArgumentException();
            }
            break;
        case DescriptorDataType.Location2d:
        case DescriptorDataType.Location2d_Rect:
        case DescriptorDataType.Location2d_Area:
        case DescriptorDataType.Location2d_Address:
            if( toValidateRaw.GetType().BaseType != typeof(Location2dDDataEntry) ) {
                throw new ArgumentException();
            }
            break;
        }

        return this.ValidateOtherWithSelf( toValidateRaw );
    }

    protected abstract bool ValidateOtherWithSelf( DescriptorDataEntry toValidateRaw );

    //public abstract int CompareTo( object? obj );
}

