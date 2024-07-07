using System.Text.Json.Serialization;
using Wayfinder.Shared.Utility;
using Wayfinder.Shared.Utility.Timeline.Data;


namespace Wayfinder.Shared.DataEntries.Descriptor;



public class DescriptorConditionsTreeEntry :
			BooleanTree<DescriptorCondition, DescriptorConditionBooleanContext> {
	public long Id { get; private set; }

	[JsonIgnore]
	public bool IsAssignedId { get; private set; } = false;



	public DescriptorConditionsTreeEntry( bool isAnd ) : base( isAnd ) { }

	public DescriptorConditionsTreeEntry( bool isAnd, long id ) : base( isAnd ) {
		this.Id = id;
		this.IsAssignedId = true;
	}
}



public class DescriptorCondition :
			TimelineEntry<TimelineDataEntry>,
			IBoolean<DescriptorConditionBooleanContext> {
	public DescriptorCondition( IEnumerable<TimelineEventEntry<TimelineDataEntry>> events ) : base( events ) {
	}

	public DescriptorCondition( long id, IEnumerable<TimelineEventEntry<TimelineDataEntry>> events ) : base( id, events ) {
	}


	public bool True( DescriptorConditionBooleanContext context ) {
		return context.Context.Contains( this );
	}
}



public class DescriptorConditionBooleanContext {
	public TimelineEntry<TimelineDataEntry> Context;



	public DescriptorConditionBooleanContext( TimelineEntry<TimelineDataEntry> context ) {
		this.Context = context;
	}
}


