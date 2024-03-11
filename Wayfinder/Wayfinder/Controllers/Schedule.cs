using Microsoft.AspNetCore.Mvc;
using Wayfinder.Client.Data;
using Wayfinder.Data;
using Wayfinder.Shared.Data.Entries.Descriptor;


namespace Wayfinder;


[ApiController]
//[Route( "schedule" )]
[Route("[controller]")]
public class ScheduleController : ControllerBase {
    //private readonly ILogger<ScheduleAPI> _logger;
    private readonly ServerDataAccess Data;



    //public ScheduleAPI( ILogger<ScheduleAPI> logger ) {
    //    this._logger = logger;
    //}
    public ScheduleController( ServerDataAccess data ) {
        this.Data = data;
    }


    [HttpPost("Create")]
    public async Task<ScheduleEntry> Create_Async( ClientDataAccess.CreateScheduleParams parameters ) {
        return await this.Data.CreateSchedule_Async( parameters );
    }

    [HttpPost("Edit")]
    public async Task Edit_Async( ClientDataAccess.AddScheduleEventsParams parameters ) {
        await this.Data.AddScheduleEvents_Async( parameters );
    }

    [HttpPost("Remove")]
    public async Task Remove_Async( ClientDataAccess.RemoveScheduleEventsParams parameters ) {
        await this.Data.RemoveScheduleEvents_Async( parameters );
    }
}
