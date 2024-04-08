using System.Net.Http.Json;
using Wayfinder.Shared.Data.Entries.Descriptor;


namespace Wayfinder.Client.Data;



public partial class ClientDataAccess {
    public class CreateDescriptorFactsParams( IEnumerable<DescriptorFacts> factses ) {
        public IEnumerable<DescriptorFacts> Factses = factses;
    }

    public async Task<DescriptorFacts> CreateDescriptorFacts_Async( CreateDescriptorFactsParams parameters ) {
        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "DescriptorFacts/Create", parameters );

        msg.EnsureSuccessStatusCode();

        DescriptorFacts? ret = await msg.Content.ReadFromJsonAsync<DescriptorFacts>();
        if( ret is null ) {
            throw new InvalidDataException( "Could not deserialize DescriptorFacts" );
        }

        return ret;
    }


    public class AddDescriptorFactsEventsParams(
            long id,
            IEnumerable<DescriptorFacts> factses ) {
        public long Id = id;
        public IEnumerable<DescriptorFacts> Factses = factses;
    }

    public async Task<DescriptorFacts> AddDescriptorFactsEvents_Async( AddDescriptorFactsEventsParams parameters ) {
        //foreach( TimelineEventEntry evt in parameters.Events ) {
        //    if( evt.Id == -1 ) {
        //        throw new InvalidDataException( "Invalid TimelineEventEntry id" );
        //    }
        //}

        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "DescriptorFacts/Edit", parameters );

        msg.EnsureSuccessStatusCode();

        DescriptorFacts? ret = await msg.Content.ReadFromJsonAsync<DescriptorFacts>();
		if( ret is null ) {
			throw new InvalidDataException( "Could not deserialize DescriptorFacts" );
		}

		return ret;
	}


    public class RemoveDescriptorFactsEventsParams(
            long id,
            IList<long> factsesIds ) {
        public long Id = id;
        public IList<long> FactsesIds = factsesIds;
    }

    public async Task RemoveDataTimelineEvents_Async( RemoveDescriptorFactsEventsParams parameters ) {
        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "DescriptorFacts/Remove", parameters );

        msg.EnsureSuccessStatusCode();
    }
}
