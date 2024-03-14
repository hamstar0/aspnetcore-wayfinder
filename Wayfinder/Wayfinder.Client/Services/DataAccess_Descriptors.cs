using System.Net.Http.Json;
using Wayfinder.Shared.Data.Entries;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Data.Schedule;
using Wayfinder.Shared.Libraries;
using Wayfinder.Shared.Libraries.BooleanTree;


namespace Wayfinder.Client.Data;


public partial class ClientDataAccess {
    public class GetDescriptorsByCriteriaParams( TermEntry subject, TermEntry relationship ) {
        public TermEntry Subject = subject;
        public TermEntry Relationship = relationship;
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
            TermEntry termSubj,
            TermEntry termRel,
            ScheduleEntry schedule,
            BooleanTree<DescriptorEntry>? conditions ) {
        public TermEntry TermSubj = termSubj;
        public TermEntry TermRel = termRel;
        public ScheduleEntry Schedule = schedule;
        public BooleanTree<DescriptorEntry>? Conditions = conditions;
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
            Optional<TermEntry> termSubj,
            Optional<TermEntry> termRel,
            Optional<ScheduleEntry> sched,
            Optional<BooleanTree<DescriptorEntry>?> conds ) {
        public long Id = id;
        public Optional<TermEntry> TermSubj = termSubj;
        public Optional<TermEntry> TermRel = termRel;
        public Optional<ScheduleEntry> Schedule = sched;
        public Optional<BooleanTree<DescriptorEntry>?> Conditions = conds;
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
