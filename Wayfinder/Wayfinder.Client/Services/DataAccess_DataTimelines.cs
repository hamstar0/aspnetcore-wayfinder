using System.Net.Http.Json;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Utility;
using Wayfinder.Shared.Utility.Timeline.Data;


namespace Wayfinder.Client.Data;



public partial class ClientDataAccess {
    public class CreateDescriptorFactsParams( IEnumerable<TimelineEventEntry<TimelineDataEntry>> factses ) {
        public IEnumerable<TimelineEventEntry<TimelineDataEntry>> Factses = factses;
    }

    public async Task<DescriptorFactsEntry> CreateDescriptorFacts_Async( CreateDescriptorFactsParams parameters ) {
        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "DescriptorFacts/Create", parameters );

        msg.EnsureSuccessStatusCode();

        DescriptorFactsEntry? ret = await msg.Content.ReadFromJsonAsync<DescriptorFactsEntry>();
        if( ret is null ) {
            throw new InvalidDataException( "Could not deserialize DescriptorFacts" );
        }

        return ret;
    }


    public class AddDescriptorFactsEventsParams(
            long id,
            IEnumerable<TimelineEventEntry<TimelineDataEntry>> factses ) {
        public long Id = id;
        public IEnumerable<TimelineEventEntry<TimelineDataEntry>> Factses = factses;
    }

    public async Task<DescriptorFactsEntry> AddDescriptorFactsEvents_Async( AddDescriptorFactsEventsParams parameters ) {
        //foreach( TimelineEventEntry evt in parameters.Events ) {
        //    if( evt.Id == -1 ) {
        //        throw new InvalidDataException( "Invalid TimelineEventEntry id" );
        //    }
        //}

        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "DescriptorFacts/Edit", parameters );

        msg.EnsureSuccessStatusCode();

        DescriptorFactsEntry? ret = await msg.Content.ReadFromJsonAsync<DescriptorFactsEntry>();
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
