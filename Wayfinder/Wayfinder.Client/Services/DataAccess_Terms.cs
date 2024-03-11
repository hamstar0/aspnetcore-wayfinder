using System.Net.Http.Json;
using Wayfinder.Shared.Data.Entries;


namespace Wayfinder.Client.Data;



public partial class ClientDataAccess {
    public class GetTermsByCriteriaParams(
                string termPattern,
                TermEntry? context ) {
        public string TermPattern = termPattern;
        public TermEntry? Context = context;
    }
    
    public async Task<IEnumerable<TermEntry>> GetTermsByCriteria_Async( GetTermsByCriteriaParams parameters ) {
        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "Term/GetByCriteria", parameters );

        msg.EnsureSuccessStatusCode();

        IEnumerable<TermEntry>? ret = await msg.Content.ReadFromJsonAsync<IEnumerable<TermEntry>>();
        if( ret is null ) {
            throw new InvalidDataException( "Could not deserialize IEnumerable<TermEntry>" );
        }

        return ret;
    }


    public class CreateTermParams(
                string termPattern,
                TermEntry? context,
                TermEntry? alias ) {
        public string TermPattern = termPattern;
        public TermEntry? Context = context;
        public TermEntry? Alias = alias;
    }
    
    public async Task<TermEntry> CreateTerm_Async( CreateTermParams parameters ) {
        HttpResponseMessage msg = await this.Http.PostAsJsonAsync( "Term/Create", parameters );

        msg.EnsureSuccessStatusCode();

        TermEntry? ret = await msg.Content.ReadFromJsonAsync<TermEntry>();
        if( ret is null ) {
            throw new InvalidDataException( "Could not deserialize TermEntry" );
        }

        return ret;
    }
}
