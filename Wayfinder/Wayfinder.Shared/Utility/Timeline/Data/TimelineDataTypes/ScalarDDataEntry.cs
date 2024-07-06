using System.ComponentModel.DataAnnotations;
using Wayfinder.Shared.Utility.Timeline.Data;


namespace Wayfinder.Shared.Utility.Timeline.Data.TimelineDataTypes;



public class ScalarDDataEntry : TimelineDataEntry, IEquatable<ScalarDDataEntry> {
	public double Value { get; private set; }



	public ScalarDDataEntry( double value ) : base( DescriptorDataType.ScalarRange ) {
		Value = value;
	}

	public bool Equals( ScalarDDataEntry? other ) {
		if( other is null ) { return false; }
		//if( this.For != other.For ) { return false; }
		if( Value != other.Value ) { return false; }
		return true;
	}


	protected override bool ValidateOtherWithSelf( TimelineDataEntry rawValidator ) {
		if( rawValidator is null || rawValidator is not ScalarRangeDDataEntry ) {
			throw new ArgumentException();
		}
		var validator = (ScalarRangeDDataEntry)rawValidator;

		return Value < validator.MinValue
			? false
			: Value > validator.MaxValue
			? false
			: true;
	}
}
