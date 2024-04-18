using Wayfinder.Shared.Data.Entries;
using Wayfinder.Client.Data;


namespace Wayfinder.Data;


public partial class ServerDataAccess {
    private long CurrentTermId = 0;
    private IDictionary<long, TermEntry> Terms = new Dictionary<long, TermEntry>();



    public async Task<IEnumerable<TermEntry>> GetTermsByCriteria_Async(
                ClientDataAccess.GetTermsByCriteriaParams parameters ) {
		var terms = this.Terms.Values
			.Where( t => t.DeepCompare(parameters.TermPattern, parameters.Context) );

		return terms;
	}


	public async Task<TermEntry> CreateTerm_Async( ClientDataAccess.CreateTermParams parameters ) {
		var term = new TermEntry(
			id: this.CurrentTermId++,
			term: parameters.TermPattern,
			context: parameters.Context,
			alias: parameters.Alias
		);

		this.Terms[ term.Id ] = term;

		return term;
	}
}
