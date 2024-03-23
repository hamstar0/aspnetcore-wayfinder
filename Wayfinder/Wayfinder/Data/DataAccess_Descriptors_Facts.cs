using Wayfinder.Client.Data;
using Wayfinder.Shared.Data.Entries.Descriptor;


namespace Wayfinder.Data;


public partial class ServerDataAccess {
	private IDictionary<long, DescriptorEntry> FactualDescriptors = new Dictionary<long, DescriptorEntry>();


	public async Task<IEnumerable<DescriptorEntry>> GetFactDescriptorsByCriteria_Async(
				ClientDataAccess.GetDescriptorsByCriteriaParams parameters ) {
		IEnumerable<DescriptorEntry> descriptors = this.Descriptors.Values;

		if( parameters.Facts.HasValue ) {
            descriptors = descriptors.Where( d => parameters.Facts.Value!.ContainsTimeline(d.Facts) );
        }
		if( parameters.Conditions.HasValue ) {
            descriptors = descriptors.Where( d => parameters.Conditions.Value!.Equals(d.Conditions) );
        }

		return descriptors;
    }


    public async Task<bool> PromoteDescriptorToFact_Async( long id ) {
        if( !this.Descriptors.ContainsKey(id) ) {
            return false;
        }

        this.FactualDescriptors[id] = this.Descriptors[id];

        return true;
    }
}
