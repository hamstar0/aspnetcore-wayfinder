using System.Collections;


namespace Wayfinder.Shared.Utility;



public class TimelineEvent<T> {
	private static long CurrentAutoId = 1;
	

	public long Id { get; private set; }

	public bool IsAssignedId { get; private set; } = false;

	public DateTime StartTime;

	public DateTime EndTime;

	public T Data;


	public TimelineEvent( DateTime start, DateTime end, T data ) {
		this.Id = TimelineEvent<T>.CurrentAutoId++;
		this.StartTime = start;
		this.EndTime = end;
		this.Data = data;
	}

	public TimelineEvent( long id, DateTime start, DateTime end, T data ) : this( start, end, data ) {
		this.Id = id;
		this.IsAssignedId = true;
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

	public void GetNewAutoId() {
		this.Id = TimelineEvent<T>.CurrentAutoId++;
		this.IsAssignedId = false;
	}
}
