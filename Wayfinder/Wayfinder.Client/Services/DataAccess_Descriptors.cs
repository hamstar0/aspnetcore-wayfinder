using System.Net.Http.Json;
using Wayfinder.Shared.Data;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Utility;


namespace Wayfinder.Client.Data;



public partial class ClientDataAccess {
    public class GetDescriptorsByCriteriaParams(
            Optional<DataTimelineEntry> facts,
            Optional<DescriptorConditionsTree> conditions ) {
        public Optional<DataTimelineEntry> Facts = facts;
        public Optional<DescriptorConditionsTree> Conditions = conditions;
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
            DataTimelineEntry facts,
            DescriptorConditionsTree conditions ) {
        public DataTimelineEntry Facts = facts;
        public DescriptorConditionsTree Conditions = conditions;
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
            Optional<DataTimelineEntry> facts,
            Optional<DescriptorConditionsTree> conds ) {
        public long Id = id;
        public Optional<DataTimelineEntry> Facts = facts;
        public Optional<DescriptorConditionsTree> Conditions = conds;
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
