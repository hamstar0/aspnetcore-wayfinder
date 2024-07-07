using System.Text.Json.Serialization;
using Wayfinder.Shared.Utility.DataStructures;
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
			TimelineEntry<TimelineEventDataEntry>,
			IBoolean<DescriptorConditionBooleanContext> {
	public DescriptorCondition( IEnumerable<TimelineEventEntry<TimelineEventDataEntry>> events ) : base( events ) {
	}

	public DescriptorCondition( long id, IEnumerable<TimelineEventEntry<TimelineEventDataEntry>> events ) : base( id, events ) {
	}


	public bool True( DescriptorConditionBooleanContext context ) {
		return context.Context.Contains( this );
	}
}



public class DescriptorConditionBooleanContext {
	public TimelineEntry<TimelineEventDataEntry> Context;



	public DescriptorConditionBooleanContext( TimelineEntry<TimelineEventDataEntry> context ) {
		this.Context = context;
	}
}


