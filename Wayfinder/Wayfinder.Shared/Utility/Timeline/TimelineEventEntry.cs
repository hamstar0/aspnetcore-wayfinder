using System.Collections;


namespace Wayfinder.Shared.Utility;



public class TimelineEventEntry<T> {
	private static long CurrentAutoId = 1;
	

	public long Id { get; private set; }

	public bool IsAssignedId { get; private set; } = false;

	public DateTime StartTime;

	public DateTime EndTime;

	public T Data;


	public TimelineEventEntry( DateTime start, DateTime end, T data ) {
		this.Id = TimelineEventEntry<T>.CurrentAutoId++;
		this.StartTime = start;
		this.EndTime = end;
		this.Data = data;
	}

	public TimelineEventEntry( long id, DateTime start, DateTime end, T data ) : this( start, end, data ) {
		this.Id = id;
		this.IsAssignedId = true;
		this.StartTime = start;
		this.EndTime = end;
		this.Data = data;
	}

	public bool Equals( TimelineEventEntry<T>? other ) {
		if( other is null ) { return false; }
		if( this.StartTime != other.StartTime ) { return false; }
		if( this.EndTime != other.EndTime ) { return false; }
		if( this.Data is not null ) {
			if( !this.Data.Equals(other.Data) ) { return false; }
		}
		return true;
	}

	public void GetNewAutoId() {
		this.Id = TimelineEventEntry<T>.CurrentAutoId++;
		this.IsAssignedId = false;
	}
}
