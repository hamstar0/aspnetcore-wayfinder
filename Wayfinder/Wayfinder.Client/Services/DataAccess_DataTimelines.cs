using System.Net.Http.Json;
using Wayfinder.Shared.Data;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Libraries;


namespace Wayfinder.Client.Data;



public partial class ClientDataAccess {
    public class CreateDataTimelineParams( IEnumerable<TimelineEvent<DescriptorDataEntry>> evts ) {
        public IEnumerable<TimelineEvent<DescriptorDataEntry>> Events = evts;
    }

    public async Task<DataTimelineEntry> CreateSchedule_Async( CreateDataTimelineParams parameters ) {
        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "Schedule/Create", parameters );

        msg.EnsureSuccessStatusCode();

        DataTimelineEntry? ret = await msg.Content.ReadFromJsonAsync<DataTimelineEntry>();
        if( ret is null ) {
            throw new InvalidDataException( "Could not deserialize ScheduleEntry" );
        }

        return ret;
    }


    public class AddDataTimelineEventsParams(
            long dataTimelineId,
            IEnumerable<TimelineEvent<DescriptorDataEntry>> evts ) {
        public long DataTimelineId = dataTimelineId;
        public IEnumerable<TimelineEvent<DescriptorDataEntry>> Events = evts;
    }

    public async Task<DataTimelineEntry> AddScheduleEvents_Async( AddDataTimelineEventsParams parameters ) {
        //foreach( ScheduleEventEntry evt in parameters.Events ) {
        //    if( evt.Id == -1 ) {
        //        throw new InvalidDataException( "Invalid ScheduleEventEntry id" );
        //    }
        //}

        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "Schedule/Edit", parameters );

        msg.EnsureSuccessStatusCode();

		DataTimelineEntry? ret = await msg.Content.ReadFromJsonAsync<DataTimelineEntry>();
		if( ret is null ) {
			throw new InvalidDataException( "Could not deserialize ScheduleEntry" );
		}

		return ret;
	}


    public class RemoveDataTimelineEventsParams(
            long scheduleId,
            IList<long> eventIds ) {
        public long DataTimelineId = scheduleId;
        public IList<long> EventIds = eventIds;
    }

    public async Task RemoveScheduleEvents_Async( RemoveDataTimelineEventsParams parameters ) {
        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "Schedule/Remove", parameters );

        msg.EnsureSuccessStatusCode();
    }
}
