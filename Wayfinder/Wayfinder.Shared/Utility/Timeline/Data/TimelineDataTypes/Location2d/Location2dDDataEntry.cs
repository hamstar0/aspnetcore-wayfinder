using System;
using Wayfinder.Shared.Utility.Timeline.Data;


namespace Wayfinder.Shared.Utility.Timeline.Data.TimelineDataTypes.Location2d;



public abstract class Location2dDDataEntry : ITimelineDataEntry {
	protected Location2dDDataEntry() { }


	public abstract bool Contains( ITimelineDataEntry data );
}

