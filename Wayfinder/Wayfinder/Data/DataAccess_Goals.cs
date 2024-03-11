using Wayfinder.Client.Data;
using Wayfinder.Shared.Data.Entries;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Libraries;


namespace Wayfinder.Data;


public partial class ServerDataAccess {
    private long CurrentGoalId = 0;
    private IDictionary<long, GoalEntry> Goals = new Dictionary<long, GoalEntry>();



    public async Task<GoalEntry?> GetGoalById_Async( ClientDataAccess.GetGoalByIdParams parameters ) {
        GoalEntry? goal = this.Goals.ContainsKey( parameters.Id ) ? this.Goals[parameters.Id] : null;

        return goal;
    }

    public async Task<GoalEntry?> GetGoalByName_Async( ClientDataAccess.GetGoalByNameParams parameters ) {
        GoalEntry? goal = this.Goals.Values
            .FirstOrDefault( g => g.Name == parameters.Name );

        return goal;
    }

    public async Task<IEnumerable<GoalEntry>> GetGoalsByCriteria_Async(
                ClientDataAccess.GetGoalsByCriteriaParams parameters ) {
        IEnumerable<GoalEntry> goals = this.Goals.Values;

        if( parameters.NamePattern.HasValue ) {
            goals = goals.Where( g => g.Name.Contains( parameters.NamePattern.Value! ) );
        }
        if( parameters.DescPattern.HasValue ) {
            if( parameters.DescPattern.Value is null ) {
                goals = goals.Where( g => parameters.DescPattern.Value is null );
            }
            else {
                goals = goals.Where( g => g.Name.Contains(parameters.DescPattern.Value) );
            }
        }
        if( parameters.Subject.HasValue ) {
            goals = goals.Where( g => g.Needed.TermSubj.Equals(parameters.Subject) );
        }
        if( parameters.Relationship.HasValue ) {
            goals = goals.Where( g => g.Needed.TermRel.Equals(parameters.Relationship) );
        }

        return goals;
    }


    public async Task<GoalEntry> CreateGoal_Async( ClientDataAccess.CreateGoalParams parameters ) {
        var goal = new GoalEntry {
            Id = this.CurrentGoalId++,
            Name = parameters.Name,
            Description = parameters.Description,
            Needed = parameters.Needed
        };

        this.Goals[goal.Id] = goal;

        return goal;
    }
}
