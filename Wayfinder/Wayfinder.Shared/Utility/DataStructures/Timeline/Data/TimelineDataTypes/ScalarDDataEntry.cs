using System.ComponentModel.DataAnnotations;


namespace Wayfinder.Shared.Utility.Timeline.Data.TimelineDataTypes;



public class ScalarDDataEntry : ITimelineDataEntry, IEquatable<ScalarDDataEntry> {
	public double Value { get; private set; }



	public ScalarDDataEntry( double value ) {
		Value = value;
	}

	public bool Equals( ScalarDDataEntry? other ) {
		if( other is null ) { return false; }
		//if( this.For != other.For ) { return false; }
		if( Value != other.Value ) { return false; }
		return true;
	}


	public bool Contains( ITimelineDataEntry data ) {
		var validator = data as ScalarRangeDDataEntry;
		if( validator is null ) { return false; }

		return this.Value < validator.MinValue
			? false
			: this.Value > validator.MaxValue
			? false
			: true;
	}
}
