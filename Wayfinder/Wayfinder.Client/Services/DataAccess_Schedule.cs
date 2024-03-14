using System.Net.Http.Json;
using Wayfinder.Shared.Data.Entries;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Data.Schedule;
using Wayfinder.Shared.Libraries;


namespace Wayfinder.Client.Data;



public partial class ClientDataAccess {
    public class CreateScheduleParams( IEnumerable<ScheduleEventEntry> evts ) {
        public IEnumerable<ScheduleEventEntry> Events = evts;
    }

    public async Task<ScheduleEntry> CreateSchedule_Async( CreateScheduleParams parameters ) {
        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "Schedule/Create", parameters );

        msg.EnsureSuccessStatusCode();

        ScheduleEntry? ret = await msg.Content.ReadFromJsonAsync<ScheduleEntry>();
        if( ret is null ) {
            throw new InvalidDataException( "Could not deserialize ScheduleEntry" );
        }

        return ret;
    }


    public class AddScheduleEventsParams(
            long scheduleId,
            IEnumerable<TimelineEvent<DescriptorDataEntry>> evts ) {
        public long ScheduleId = scheduleId;
        public IEnumerable<TimelineEvent<DescriptorDataEntry>> Events = evts;
    }

    public async Task<ScheduleEntry> AddScheduleEvents_Async( AddScheduleEventsParams parameters ) {
        //foreach( ScheduleEventEntry evt in parameters.Events ) {
        //    if( evt.Id == -1 ) {
        //        throw new InvalidDataException( "Invalid ScheduleEventEntry id" );
        //    }
        //}

        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "Schedule/Edit", parameters );

        msg.EnsureSuccessStatusCode();

		ScheduleEntry? ret = await msg.Content.ReadFromJsonAsync<ScheduleEntry>();
		if( ret is null ) {
			throw new InvalidDataException( "Could not deserialize ScheduleEntry" );
		}

		return ret;
	}


    public class RemoveScheduleEventsParams(
            long scheduleId,
            IList<long> eventIds ) {
        public long ScheduleId = scheduleId;
        public IList<long> EventIds = eventIds;
    }

    public async Task RemoveScheduleEvents_Async( RemoveScheduleEventsParams parameters ) {
        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "Schedule/Remove", parameters );

        msg.EnsureSuccessStatusCode();
    }
}
