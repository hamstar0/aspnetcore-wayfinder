using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Wayfinder.Shared.Data.Entries;
using Wayfinder.Client.Data;


namespace Wayfinder.Client.Components.Application.Editors;



public partial class TermEditor {
    //[Inject]
    //public IJSRuntime Js { get; set; } = null!;

    [Inject]
    public ClientDataAccess Data { get; set; } = null!;


    private ElementReference InputElement;

    private bool IsSeachFocused = false;

    private IEnumerable<TermEntry> SearchOptions = new List<TermEntry>();


    [Parameter]
    public bool Disabled { get; set; } = false;

    [Parameter, EditorRequired]
    public string Label { get; set; } = null!;
    
    [Parameter]
    public string? Description { get; set; }

    [Parameter, EditorRequired]
    public Func<TermEntry, Task> OnTermSelect { get; set; } = null!;



    private async Task UpdateTermSearchResults_UI_Async( string? termText ) {
        if( this.Disabled ) {
            return;
        }

        if( termText is not null ) {
            this.SearchOptions = await this.Data.GetTermsByCriteria_Async(
                new ClientDataAccess.GetTermsByCriteriaParams(termText, null)
            );
        }
        else {
            this.SearchOptions = new List<TermEntry>();
        }
    }


    private async Task InteractSearchResults_UI_Async( KeyboardEventArgs arg ) {
        if( this.Disabled ) {
            return;
        }
        if( !this.IsSeachFocused ) {
            return;
        }

        //todo search results navigation
    }

    private async Task SelectSearchResults_UI_Async( TermEntry term ) {
        if( this.Disabled ) {
            return;
        }
        if( !this.IsSeachFocused ) {
            return;
        }

        await this.OnTermSelect( term );
    }

    private async Task SubmitNewTerm_UI_Async( string termText ) {
        if( this.Disabled ) {
            return;
        }

        TermEntry? newTerm = await this.Data.CreateTerm_Async(
            new ClientDataAccess.CreateTermParams(termText, null, null)
        );

        if( newTerm is not null ) {
            await this.OnTermSelect( newTerm );
        }
    }
}
