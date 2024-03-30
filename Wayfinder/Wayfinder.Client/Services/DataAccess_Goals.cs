using System.Linq;
using System.Net.Http.Json;
using Wayfinder.Shared.Data.Entries;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Utility;


namespace Wayfinder.Client.Data;


public partial class ClientDataAccess {
    public class GetGoalByIdParams( long id ) {
        public long Id = id;
    }

    public async Task<GoalEntry?> GetGoalById_Async( GetGoalByIdParams parameters ) {
		HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "Goal/GetById", parameters );

        msg.EnsureSuccessStatusCode();

        GoalEntry? ret = await msg.Content.ReadFromJsonAsync<GoalEntry>();
        if( ret is null ) {
            throw new InvalidDataException( "Could not deserialize GoalEntry" );
        }

		return ret;
	}


    public class GetGoalByNameParams( string name ) {
        public string Name = name;
    }

    public async Task<GoalEntry?> GetGoalByName_Async( GetGoalByNameParams parameters ) {
        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "Goal/GetByName", parameters );

        msg.EnsureSuccessStatusCode();

        GoalEntry? ret = await msg.Content.ReadFromJsonAsync<GoalEntry>();
        if( ret is null ) {
            throw new InvalidDataException( "Could not deserialize GoalEntry" );
        }

        return ret;
    }


    public class GetGoalsByCriteriaParams(
                Optional<string> namePattern,
                Optional<string?> descPattern = default,
                Optional<TermEntry> subj = default,
                Optional<TermEntry> rel = default ) {
        public Optional<string> NamePattern = namePattern;
        public Optional<string?> DescPattern = descPattern;
        public Optional<TermEntry> Subject = subj;
        public Optional<TermEntry> Relationship = rel;
    }
    
    public async Task<IEnumerable<GoalEntry>> GetGoalsByCriteria_Async( GetGoalsByCriteriaParams parameters ) {
        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "Goal/GetByCriteria", parameters );

        msg.EnsureSuccessStatusCode();

        IEnumerable<GoalEntry>? ret = await msg.Content.ReadFromJsonAsync<IEnumerable<GoalEntry>>();
        if( ret is null ) {
            throw new InvalidDataException( "Could not deserialize IEnumerable<GoalEntry>" );
        }

        return ret;
    }


    public class CreateGoalParams(
                string name,
                string? desc,
                DescriptorEntry needed ) {
        public string Name = name;
        public string? Description = desc;
        public DescriptorEntry Needed = needed;
    }
    
    public async Task<GoalEntry> CreateGoal_Async( CreateGoalParams parameters ) {
        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "Goal/Create", parameters );

        msg.EnsureSuccessStatusCode();

        GoalEntry? ret = await msg.Content.ReadFromJsonAsync<GoalEntry>();
        if( ret is null ) {
            throw new InvalidDataException( "Could not deserialize GoalEntry" );
        }

        return ret;
    }
}
