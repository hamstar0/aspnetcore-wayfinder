using Wayfinder.Client.Data;
using Wayfinder.Shared.Data;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Libraries;


namespace Wayfinder.Data;



public partial class ServerDataAccess {
    private long CurrentDataTimelineId = 0;
    private IDictionary<long, DataTimelineEntry> DataTimelines = new Dictionary<long, DataTimelineEntry>();



    public async Task<DataTimelineEntry> CreateDataTimeline_Async(
                ClientDataAccess.CreateDataTimelineParams parameters ) {
        long id = this.CurrentDataTimelineId++;

        this.DataTimelines[ id ] = new DataTimelineEntry( id, parameters.Events );

        return this.DataTimelines[id];
    }

    public async Task AddDataTimelineEvents_Async( ClientDataAccess.AddDataTimelineEventsParams parameters ) {
        foreach( TimelineEvent<DescriptorDataEntry> evt in parameters.Events ) {
            this.DataTimelines[ parameters.DataTimelineId ].AddEvent( evt );
        }
    }

    public async Task RemoveDataTimelineEvents_Async(
                ClientDataAccess.RemoveDataTimelineEventsParams parameters ) {
        DataTimelineEntry dataTimeline = this.DataTimelines[ parameters.DataTimelineId ];

        foreach( long eventId in parameters.EventIds ) {
            foreach( TimelineEvent<DescriptorDataEntry> evt in dataTimeline.Events ) {
                if( evt.Id == eventId ) {
                    dataTimeline.RemoveEventById( evt.Id );

                    break;
                }
            }
        }
    }
}
