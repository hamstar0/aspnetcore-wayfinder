using System.Text.Json.Serialization;
using Wayfinder.Shared.Utility;


namespace Wayfinder.Shared.Data.Entries.Descriptor;



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
            TimelineEntry<DescriptorDataEntry>,
            IBoolean<DescriptorConditionBooleanContext> {
    public DescriptorCondition( IEnumerable<TimelineEventEntry<DescriptorDataEntry>> events ) : base( events ) {
    }

    public DescriptorCondition( long id, IEnumerable<TimelineEventEntry<DescriptorDataEntry>> events ) : base( id, events ) {
    }


    public bool True( DescriptorConditionBooleanContext context ) {
    }
}



public class DescriptorConditionBooleanContext {
    //public DescriptorConditionsTreeEntry Context;



    //public DescriptorConditionBooleanContext( DescriptorConditionsTreeEntry context ) {
    //    this.Context = context;
    //}
}


