using Microsoft.AspNetCore.Mvc;
using Wayfinder.Client.Data;
using Wayfinder.Data;
using Wayfinder.Shared.Data;


namespace Wayfinder;



[ApiController]
//[Route( "DataTimeline" )]
[Route("[controller]")]
public class DataTimelineController : ControllerBase {
	//private readonly ILogger<DataTimelineController> _logger;
	private readonly ServerDataAccess Data;



	//public DataTimelineController( ILogger<DataTimelineController> logger ) {
	//    this._logger = logger;
	//}
	public DataTimelineController( ServerDataAccess data ) {
        this.Data = data;
    }


    [HttpPost("Create")]
    public async Task<DataTimelineEntry> Create_Async( ClientDataAccess.CreateDataTimelineParams parameters ) {
        return await this.Data.CreateDataTimeline_Async( parameters );
    }

    [HttpPost("Edit")]
    public async Task Edit_Async( ClientDataAccess.AddDataTimelineEventsParams parameters ) {
        await this.Data.AddDataTimelineEvents_Async( parameters );
    }

    [HttpPost("Remove")]
    public async Task Remove_Async( ClientDataAccess.RemoveDataTimelineEventsParams parameters ) {
        await this.Data.RemoveDataTimelineEvents_Async( parameters );
    }
}
