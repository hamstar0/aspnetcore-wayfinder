using System.Text.Json.Serialization;
using Wayfinder.Shared.Utility;


namespace Wayfinder.Shared.Data.Entries.Descriptor;



public class DescriptorConditionsTree :
			BooleanTree<DescriptorCondition, DescriptorConditionBooleanContext> {
	public DescriptorConditionsTree( bool isAnd ) : base( isAnd ) { }
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
}


