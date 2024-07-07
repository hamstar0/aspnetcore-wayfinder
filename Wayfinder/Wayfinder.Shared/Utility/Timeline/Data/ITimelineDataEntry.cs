using System.Collections;

namespace Wayfinder.Shared.Utility.Timeline.Data;



public interface ITimelineDataEntry {
	//public abstract int CompareTo( object? obj );

	public bool Contains( ITimelineDataEntry data );
}

