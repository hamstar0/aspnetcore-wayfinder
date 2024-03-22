using Wayfinder.Client.Data;
using Wayfinder.Shared.Data.Entries.Descriptor;


namespace Wayfinder.Data;


public partial class ServerDataAccess {
    private long CurrentDescriptorId = 0;
	private IDictionary<long, DescriptorEntry> Descriptors = new Dictionary<long, DescriptorEntry>();


	public async Task<IEnumerable<DescriptorEntry>> GetDescriptorsByCriteria_Async(
				ClientDataAccess.GetDescriptorsByCriteriaParams parameters ) {
        IEnumerable<DescriptorEntry> descriptors = this.Descriptors.Values
			.Where( d => d.TermSubj.Equals(parameters.Subject) && d.TermRel.Equals(parameters.Relationship) );

		return descriptors;
	}


	public async Task<DescriptorEntry> CreateDescriptor_Async( ClientDataAccess.CreateDescriptorParams parameters ) {
		var entry = new DescriptorEntry {
			Id = this.CurrentDescriptorId++,
			TermSubj = parameters.TermSubj,
			TermRel = parameters.TermRel,
			Schedule = parameters.Facts,
			Conditions = parameters.Conditions
        };

		this.Descriptors[entry.Id] = entry;

        return entry;
	}

	public async Task<bool> EditDescriptor_Async( ClientDataAccess.EditDescriptorParams parameters ) {
		if( !this.Descriptors.ContainsKey(parameters.Id) ) {
			return false;
		}

		if( parameters.TermSubj.HasValue ) {
			this.Descriptors[parameters.Id].TermSubj = parameters.TermSubj.Value!;
		}
		if( parameters.TermRel.HasValue ) {
			this.Descriptors[parameters.Id].TermRel = parameters.TermRel.Value!;
		}

		return true;
    }
}
