using Wayfinder.Client.Data;
using Wayfinder.Shared.Data.Entries.Descriptor;


namespace Wayfinder.Data;


public partial class ServerDataAccess {
    private long CurrentDescriptorId = 0;
	private IDictionary<long, DescriptorEntry> Descriptors = new Dictionary<long, DescriptorEntry>();


	public async Task<IEnumerable<DescriptorEntry>> GetDescriptorsByCriteria_Async(
				ClientDataAccess.GetDescriptorsByCriteriaParams parameters ) {
		IEnumerable<DescriptorEntry> descriptors = this.Descriptors.Values;

        if( parameters.Facts.HasValue ) {
            descriptors = descriptors.Where( d => parameters.Facts.Value!.ContainsTimeline( d.Facts ) );
        }
        if( parameters.Conditions.HasValue ) {
            descriptors = descriptors.Where( d => parameters.Conditions.Value!.Equals( d.Conditions ) );
        }

        return descriptors;
	}


	public async Task<DescriptorEntry> CreateDescriptor_Async(
				ClientDataAccess.CreateDescriptorParams parameters ) {
		var entry = new DescriptorEntry {
			Id = this.CurrentDescriptorId++,
			Facts = parameters.Facts,
			Conditions = parameters.Conditions
        };

		this.Descriptors[entry.Id] = entry;

        return entry;
	}

	public async Task<bool> EditDescriptor_Async( ClientDataAccess.EditDescriptorParams parameters ) {
		if( !this.Descriptors.ContainsKey(parameters.Id) ) {
			return false;
		}

		if( parameters.Facts.HasValue ) {
			this.Descriptors[parameters.Id].Facts = parameters.Facts.Value!;
		}
		if( parameters.Conditions.HasValue ) {
			this.Descriptors[parameters.Id].Conditions = parameters.Conditions.Value!;
		}

		return true;
    }
}
