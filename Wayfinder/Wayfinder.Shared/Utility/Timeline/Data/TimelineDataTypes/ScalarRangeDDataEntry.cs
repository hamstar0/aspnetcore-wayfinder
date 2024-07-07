using System.ComponentModel.DataAnnotations;
using Wayfinder.Shared.Utility.Timeline.Data;


namespace Wayfinder.Shared.Utility.Timeline.Data.TimelineDataTypes;



public class ScalarRangeDDataEntry : ITimelineDataEntry, IEquatable<ScalarRangeDDataEntry> {
	public double MinValue { get; private set; }
	public double MaxValue { get; private set; }



	public ScalarRangeDDataEntry( double minValue, double maxValue ) {
		if( minValue > maxValue ) {
			throw new ArgumentException();
		}

		this.MinValue = minValue;
		this.MaxValue = maxValue;
	}

	public bool Equals( ScalarRangeDDataEntry? other ) {
		if( other is null ) { return false; }
		//if( this.For != other.For ) { return false; }
		if( this.MinValue != other.MinValue ) { return false; }
		if( this.MaxValue != other.MaxValue ) { return false; }
		return true;
	}


	public bool Contains( ITimelineDataEntry data ) {
		var validator = data as ScalarRangeDDataEntry;
		if( validator is null ) { return false; }

		if( this.MinValue == validator.MinValue && MaxValue == validator.MaxValue ) {
			return true;//0;
		}
		if( this.MinValue <= validator.MinValue && this.MaxValue >= validator.MaxValue ) {
			return true;//1;
		}
		if( this.MinValue > validator.MinValue && this.MaxValue > validator.MaxValue ) {
			return true;//0;
		}
		if( this.MinValue < validator.MinValue && this.MaxValue < validator.MaxValue ) {
			return true;//0;
		}
		return false;//-1;
	}
}
