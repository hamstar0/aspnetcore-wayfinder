using Microsoft.AspNetCore.Mvc;
using Wayfinder.Client.Data;
using Wayfinder.Data;
using Wayfinder.Shared.Data.Entries;


namespace Wayfinder;


[ApiController]
[Route("[controller]")]
public class GoalController : ControllerBase {
    //private readonly ILogger<ScheduleAPI> _logger;
    private readonly ServerDataAccess Data;



    //public ScheduleAPI( ILogger<ScheduleAPI> logger ) {
    //    this._logger = logger;
    public GoalController( ServerDataAccess data ) {
        this.Data = data;
    }


    [HttpPost("GetById")]
    public async Task<GoalEntry?> Create_Async( ClientDataAccess.GetGoalByIdParams parameters ) {
        return await this.Data.GetGoalById_Async( parameters );
    }

    [HttpPost("GetByName")]
    public async Task<GoalEntry?> Edit_Async( ClientDataAccess.GetGoalByNameParams parameters ) {
        return await this.Data.GetGoalByName_Async( parameters );
    }
     
    [HttpPost("GetByCriteria")]
    public async Task<IEnumerable<GoalEntry>> GetByCriteria_Async(
                ClientDataAccess.GetGoalsByCriteriaParams parameters ) {
        return await this.Data.GetGoalsByCriteria_Async( parameters );
    }
    
    [HttpPost("Create")]
    public async Task<GoalEntry> Create_Async( ClientDataAccess.CreateGoalParams parameters ) {
        return await this.Data.CreateGoal_Async( parameters );
    }
}
