using System.Collections;
using Wayfinder.Shared.Utility.Timeline.Data;


namespace Wayfinder.Shared.Utility.Timeline;



public class TimelineEventEntry {
	private static long CurrentAutoId = 1;


	public long Id { get; private set; }

	public bool IsAssignedId { get; private set; } = false;

	public DateTime StartTime;

	public DateTime EndTime;

	public ITimelineDataEntry Data;


	public TimelineEventEntry( DateTime start, DateTime end, ITimelineDataEntry data ) {
		this.Id = CurrentAutoId++;
		this.StartTime = start;
		this.EndTime = end;
		this.Data = data;
	}

	public TimelineEventEntry( long id, DateTime start, DateTime end, ITimelineDataEntry data )
				: this( start, end, data ) {
		this.Id = id;
		this.IsAssignedId = true;
		this.StartTime = start;
		this.EndTime = end;
		this.Data = data;
	}

	public bool Equals( TimelineEventEntry? other ) {
		if( other is null ) { return false; }
		if( this.StartTime != other.StartTime ) { return false; }
		if( this.EndTime != other.EndTime ) { return false; }
		if( this.Data is not null ) {
			if( !this.Data.Equals( other.Data ) ) { return false; }
		}
		return true;
	}

	public void GetNewAutoId() {
		this.Id = CurrentAutoId++;
		this.IsAssignedId = false;
	}
}
