﻿using Wayfinder.Client.Data;
using Wayfinder.Shared.Data;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Libraries;


namespace Wayfinder.Data;



public partial class ServerDataAccess {
    private long CurrentSchedId = 0;
    private IDictionary<long, ScheduleEntry> Schedules = new Dictionary<long, ScheduleEntry>();



    public async Task<ScheduleEntry> CreateSchedule_Async( ClientDataAccess.CreateScheduleParams parameters ) {
        long id = this.CurrentSchedId++;

        this.Schedules[ id ] = new ScheduleEntry( id, parameters.Events );

        return this.Schedules[id];
    }

    public async Task AddScheduleEvents_Async( ClientDataAccess.AddScheduleEventsParams parameters ) {
        foreach( TimelineEvent<DescriptorDataEntry> evt in parameters.Events ) {
            this.Schedules[ parameters.ScheduleId ].AddEvent( evt );
        }
    }

    public async Task RemoveScheduleEvents_Async( ClientDataAccess.RemoveScheduleEventsParams parameters ) {
        ScheduleEntry schedule = this.Schedules[parameters.ScheduleId];

        foreach( long eventId in parameters.EventIds ) {
            foreach( TimelineEvent<DescriptorDataEntry> evt in schedule.Events ) {
                if( evt.Id == eventId ) {
                    schedule.RemoveEventById( evt.Id );

                    break;
                }
            }
        }
    }
}
