using Wayfinder.Shared.Data.Entries;


namespace Wayfinder.Client.Data;


public partial class ClientDataAccess {
    private HttpClient Http;


    public ClientDataAccess( HttpClient http ) {
        this.Http = http;
    }
}
