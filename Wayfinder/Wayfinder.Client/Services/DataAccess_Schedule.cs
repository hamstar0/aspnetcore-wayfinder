using System.Net.Http.Json;
using Wayfinder.Shared.Data.Entries;
using Wayfinder.Shared.Data.Entries.Descriptor;


namespace Wayfinder.Client.Data;



public partial class ClientDataAccess {
    public class CreateScheduleParams(
            DescriptorEntry? scheduleFor,
            IList<ScheduleEventEntry> evts ) {
        public DescriptorEntry? ScheduleFor = scheduleFor;
        public IList<ScheduleEventEntry> Events = evts;
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
            IList<ScheduleEventEntry> evts ) {
        public long ScheduleId = scheduleId;
        public IList<ScheduleEventEntry> Events = evts;
    }

    public async Task AddScheduleEvents_Async( AddScheduleEventsParams parameters ) {
        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "Schedule/Edit", parameters );

        msg.EnsureSuccessStatusCode();
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
