using Microsoft.AspNetCore.Mvc;
using Wayfinder.Client.Data;
using Wayfinder.Data;
using Wayfinder.Shared.Data;
using Wayfinder.Shared.DataEntries.Descriptor;


namespace Wayfinder;



[ApiController]
//[Route( "DescriptorFacts" )]
[Route("[controller]")]
public class DescriptorFactsController : ControllerBase {
    //private readonly ILogger<DescriptorFactsController> _logger;
    private readonly ServerDataAccess Data;



    //public DescriptorFactsController( ILogger<DescriptorFactsController> logger ) {
    //    this._logger = logger;
    //}
    public DescriptorFactsController( ServerDataAccess data ) {
        this.Data = data;
    }


    [HttpPost("Create")]
    public async Task<DescriptorFactsEntry> Create_Async( ClientDataAccess.CreateDescriptorFactsParams parameters ) {
        return await this.Data.CreateDescriptorFacts_Async( parameters );
    }

    [HttpPost("Edit")]
    public async Task Edit_Async( ClientDataAccess.AddDescriptorFactsEventsParams parameters ) {
        await this.Data.AddDescriptorFactsEvents_Async( parameters );
    }

    [HttpPost("Remove")]
    public async Task Remove_Async( ClientDataAccess.RemoveDescriptorFactsEventsParams parameters ) {
        await this.Data.RemoveDescriptorFactsEvents_Async( parameters );
    }
}
