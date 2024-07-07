using System.Collections;
using System.Text.Json.Serialization;

namespace Wayfinder.Shared.Utility.Timeline.Data;



public abstract class TimelineEventDataEntry {
	public long Id { get; protected set; }

	//[JsonIgnore]
	//public bool IsAssignedId { get; private set; } = false;


	//public abstract int CompareTo( object? obj );

	public abstract bool Contains( TimelineEventDataEntry data );
}

