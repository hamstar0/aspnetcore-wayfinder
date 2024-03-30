using System.Net.Http.Json;
using Wayfinder.Shared.Data;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Utility;


namespace Wayfinder.Client.Data;



public partial class ClientDataAccess {
    public class CreateDataTimelineParams( IEnumerable<TimelineEvent<DescriptorDataEntry>> evts ) {
        public IEnumerable<TimelineEvent<DescriptorDataEntry>> Events = evts;
    }

    public async Task<DataTimelineEntry> CreateDataTimeline_Async( CreateDataTimelineParams parameters ) {
        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "DataTimeline/Create", parameters );

        msg.EnsureSuccessStatusCode();

        DataTimelineEntry? ret = await msg.Content.ReadFromJsonAsync<DataTimelineEntry>();
        if( ret is null ) {
            throw new InvalidDataException( "Could not deserialize DataTimelineEntry" );
        }

        return ret;
    }


    public class AddDataTimelineEventsParams(
            long dataTimelineId,
            IEnumerable<TimelineEvent<DescriptorDataEntry>> evts ) {
        public long DataTimelineId = dataTimelineId;
        public IEnumerable<TimelineEvent<DescriptorDataEntry>> Events = evts;
    }

    public async Task<DataTimelineEntry> AddDataTimelineEvents_Async( AddDataTimelineEventsParams parameters ) {
        //foreach( TimelineEventEntry evt in parameters.Events ) {
        //    if( evt.Id == -1 ) {
        //        throw new InvalidDataException( "Invalid TimelineEventEntry id" );
        //    }
        //}

        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "DataTimeline/Edit", parameters );

        msg.EnsureSuccessStatusCode();

		DataTimelineEntry? ret = await msg.Content.ReadFromJsonAsync<DataTimelineEntry>();
		if( ret is null ) {
			throw new InvalidDataException( "Could not deserialize DataTimelineEntry" );
		}

		return ret;
	}


    public class RemoveDataTimelineEventsParams(
            long timelineId,
            IList<long> eventIds ) {
        public long TimelineId = timelineId;
        public IList<long> EventIds = eventIds;
    }

    public async Task RemoveDataTimelineEvents_Async( RemoveDataTimelineEventsParams parameters ) {
        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "DataTimeline/Remove", parameters );

        msg.EnsureSuccessStatusCode();
    }
}
