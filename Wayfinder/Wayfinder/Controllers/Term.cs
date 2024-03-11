using Microsoft.AspNetCore.Mvc;
using Wayfinder.Client.Data;
using Wayfinder.Data;
using Wayfinder.Shared.Data.Entries;


namespace Wayfinder;


[ApiController]
[Route("[controller]")]
public class TermController : ControllerBase {
    //private readonly ILogger<ScheduleAPI> _logger;
    private readonly ServerDataAccess Data;



    //public ScheduleAPI( ILogger<ScheduleAPI> logger ) {
    //    this._logger = logger;
    public TermController( ServerDataAccess data ) {
        this.Data = data;
    }


    [HttpPost("GetByCriteria")]
    public async Task<IEnumerable<TermEntry>> GetByCriteria_Async(
                ClientDataAccess.GetTermsByCriteriaParams parameters ) {
        return await this.Data.GetTermsByCriteria_Async( parameters );
    }

    [HttpPost("Create")]
    public async Task<TermEntry> Create_Async( ClientDataAccess.CreateTermParams parameters ) {
        return await this.Data.CreateTerm_Async( parameters );
    }
}
