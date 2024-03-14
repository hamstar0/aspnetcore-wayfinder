using System.Collections;


namespace Wayfinder.Shared.Libraries;



public class TimelineEvent<T> {
	public DateTime StartTime;

	public DateTime EndTime;

	public T Data;


	public TimelineEvent( DateTime start, DateTime end, T data ) {
		this.StartTime = start;
		this.EndTime = end;
		this.Data = data;
	}

	public bool Equals( TimelineEvent<T>? other ) {
		if( other is null ) { return false; }
		if( this.StartTime != other.StartTime ) { return false; }
		if( this.EndTime != other.EndTime ) { return false; }
		if( this.Data is not null ) {
			if( !this.Data.Equals(other.Data) ) { return false; }
		}
		return true;
	}
}
