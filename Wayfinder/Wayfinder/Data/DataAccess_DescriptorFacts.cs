using Wayfinder.Client.Data;
using Wayfinder.Shared.Data;
using Wayfinder.Shared.DataEntries.Descriptor;
using Wayfinder.Shared.Utility.Timeline.Data;


namespace Wayfinder.Data;



public partial class ServerDataAccess {
    private long CurrentDataTimelineId = 0;
    private IDictionary<long, DescriptorFactsEntry> FactsesById = new Dictionary<long, DescriptorFactsEntry>();



    public async Task<DescriptorFactsEntry> CreateDescriptorFacts_Async(
                ClientDataAccess.CreateDescriptorFactsParams parameters ) {
        long id = this.CurrentDataTimelineId++;

        this.FactsesById[ id ] = new DescriptorFactsEntry( id, parameters.Factses );

        return this.FactsesById[id];
    }

    public async Task AddDescriptorFactsEvents_Async( ClientDataAccess.AddDescriptorFactsEventsParams parameters ) {
        foreach( TimelineEventEntry<TimelineDataEntry> evt in parameters.Factses ) {
            this.FactsesById[ parameters.Id ].AddEvent( evt );
        }
    }

    public async Task RemoveDescriptorFactsEvents_Async(
                ClientDataAccess.RemoveDescriptorFactsEventsParams parameters ) {
        DescriptorFactsEntry facts = this.FactsesById[ parameters.Id ];

        foreach( long id in parameters.FactsesIds ) {
            foreach( TimelineEventEntry<TimelineDataEntry> fact in facts.Events ) {
                if( fact.Id == id ) {
                    facts.RemoveEventById( fact.Id );

                    break;
                }
            }
        }
    }
}
