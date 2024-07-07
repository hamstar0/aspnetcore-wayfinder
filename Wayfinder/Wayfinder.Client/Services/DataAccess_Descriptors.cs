using System.Net.Http.Json;
using Wayfinder.Shared.Data;
using Wayfinder.Shared.DataEntries.Descriptor;
using Wayfinder.Shared.Utility;


namespace Wayfinder.Client.Data;



public partial class ClientDataAccess {
    public class GetDescriptorsByCriteriaParams(
            Optional<DescriptorFactsEntry> facts,
            Optional<DescriptorConditionsTreeEntry> conditions ) {
        public Optional<DescriptorFactsEntry> Facts = facts;
        public Optional<DescriptorConditionsTreeEntry> Conditions = conditions;
    }

    public async Task<IEnumerable<DescriptorEntry>> GetDescriptorsByCriteria_Async(
            GetDescriptorsByCriteriaParams parameters ) {
		HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "Descriptor/GetByCriteria", parameters );

        msg.EnsureSuccessStatusCode();

        IEnumerable<DescriptorEntry>? ret = await msg.Content.ReadFromJsonAsync<IEnumerable<DescriptorEntry>>();
        if( ret is null ) {
            throw new InvalidDataException( "Could not deserialize IEnumerable<DescriptorEntry>" );
        }

        return ret;
    }


    public class CreateDescriptorParams(
            DescriptorFactsEntry facts,
            DescriptorConditionsTreeEntry conditions ) {
        public DescriptorFactsEntry Facts = facts;
        public DescriptorConditionsTreeEntry Conditions = conditions;
    }

    public async Task<DescriptorEntry> CreateDescriptor_Async( CreateDescriptorParams parameters ) {
        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "Descriptor/Create", parameters );

        msg.EnsureSuccessStatusCode();

        DescriptorEntry? ret = await msg.Content.ReadFromJsonAsync<DescriptorEntry>();
        if( ret is null ) {
            throw new InvalidDataException("Could not deserialize DescriptorEntry");
        }

        return ret;
    }


    public class EditDescriptorParams(
            long id,
            Optional<DescriptorFactsEntry> facts,
            Optional<DescriptorConditionsTreeEntry> conds ) {
        public long Id = id;
        public Optional<DescriptorFactsEntry> Facts = facts;
        public Optional<DescriptorConditionsTreeEntry> Conditions = conds;
    }

    public async Task<bool> EditDescriptor_Async( EditDescriptorParams parameters ) {
        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "Descriptor/Edit", parameters );

        msg.EnsureSuccessStatusCode();

        bool? ret = await msg.Content.ReadFromJsonAsync<bool>();
        if( ret is null ) {
            throw new InvalidDataException( "Could not deserialize bool" );
        }

        return ret ?? false;
    }
}
