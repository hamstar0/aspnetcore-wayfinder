using System.Net.Http.Json;
using Wayfinder.Shared.Data.Entries;
using Wayfinder.Shared.Utility;


namespace Wayfinder.Client.Data;



public partial class ClientDataAccess {
    public class CreatePlanParams(
                string? name,
                GoalEntry goal,
				ISet<PlanOptionEntry> options,
                TimelineEntry<PlanOptionEntry> optionTimeline ) {
        public string? Name = name;
        public GoalEntry Goal = goal;
        public ISet<PlanOptionEntry> Options = options;
        public TimelineEntry<PlanOptionEntry> OptionTimeline = optionTimeline;
    }
    
    public async Task<PlanEntry> CreatePlan_Async( CreatePlanParams parameters ) {
        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "Plan/Create", parameters );

        msg.EnsureSuccessStatusCode();

        PlanEntry? ret = await msg.Content.ReadFromJsonAsync<PlanEntry>();
        if( ret is null ) {
            throw new InvalidDataException( "Could not deserialize PlanEntry" );
        }

        return ret;
    }

    
    public class EditPlanParams(
			    Optional<string?> name,
			    Optional<ISet<PlanOptionEntry>> options,
			    Optional<TimelineEntry<PlanOptionEntry>> optionTimeline ) {
        public Optional<string?> Name = name;
        public Optional<ISet<PlanOptionEntry>> Options = options;
        public Optional<TimelineEntry<PlanOptionEntry>> OptionTimeline = optionTimeline;
    }
    
    public async Task EditPlan_Async( EditPlanParams parameters ) {
        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "Plan/Edit", parameters );

        msg.EnsureSuccessStatusCode();
    }
}
