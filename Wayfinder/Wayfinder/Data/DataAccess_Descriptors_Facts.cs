using Wayfinder.Client.Data;
using Wayfinder.Shared.Data.Entries.Descriptor;


namespace Wayfinder.Data;


public partial class ServerDataAccess {
	private IDictionary<long, DescriptorEntry> FactualDescriptors = new Dictionary<long, DescriptorEntry>();


	public async Task<IEnumerable<DescriptorEntry>> GetFactDescriptorsByCriteria_Async(
				ClientDataAccess.GetDescriptorsByCriteriaParams parameters ) {
		IEnumerable<DescriptorEntry> descriptors = this.Descriptors.Values;

		if( parameters.FactValidator.HasValue ) {
            descriptors = descriptors.Where( d => parameters.FactValidator.Value!.Validate(d.Facts) );
        }
		if( parameters.Conditions.HasValue ) {
            descriptors = descriptors.Where( d => parameters.Conditions.Value!.Equals(d.Conditions) );
        }

		return descriptors;
	}


	public async Task<bool> PromoptDescriptor_Async( long id ) {
		if( !this.Descriptors.ContainsKey(id) ) {
			return false;
		}

		return true;
    }
}
