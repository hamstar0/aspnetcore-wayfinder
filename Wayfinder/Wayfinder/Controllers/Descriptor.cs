using Microsoft.AspNetCore.Mvc;
using Wayfinder.Client.Data;
using Wayfinder.Data;
using Wayfinder.Shared.Data.Entries.Descriptor;


namespace Wayfinder;


[ApiController]
[Route("[controller]")]
public class DescriptorController : ControllerBase {
    //private readonly ILogger<ScheduleAPI> _logger;
    private readonly ServerDataAccess Data;



    //public ScheduleAPI( ILogger<ScheduleAPI> logger ) {
    //    this._logger = logger;
    public DescriptorController( ServerDataAccess data ) {
        this.Data = data;
    }


    [HttpPost("GetByCriteria")]
    public async Task<IEnumerable<DescriptorEntry>> Create_Async(
                ClientDataAccess.GetDescriptorsByCriteriaParams parameters ) {
        return await this.Data.GetDescriptorsByCriteria_Async( parameters );
    }

    [HttpPost("Create")]
    public async Task<DescriptorEntry> Edit_Async( ClientDataAccess.CreateDescriptorParams parameters ) {
        return await this.Data.CreateDescriptor_Async( parameters );
    }

    [HttpPost("Edit")]
    public async Task<bool> Remove_Async( ClientDataAccess.EditDescriptorParams parameters ) {
        return await this.Data.EditDescriptor_Async( parameters );
    }
}
