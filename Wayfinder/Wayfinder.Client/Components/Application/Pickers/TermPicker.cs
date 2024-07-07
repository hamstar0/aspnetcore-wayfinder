using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Wayfinder.Client.Data;
using Wayfinder.Shared.DataEntries;


namespace Wayfinder.Client.Components.Application.Pickers;


public partial class TermPicker {
    //[Inject]
    //public IJSRuntime Js { get; set; } = null!;

    [Inject]
    public ClientDataAccess Data { get; set; } = null!;


    private ElementReference InputElement;
    
    private bool IsSeachFocused = false;

    private string SearchText = "";

    private bool IsCurrentInputSuppressed = false;

    private TermEntry? SelectedTerm = null;

    private IList<TermEntry> SearchOptions = new List<TermEntry>();

    private int SearchPosition = -1;


    [Parameter]
    public bool Disabled { get; set; } = false;

    [Parameter, EditorRequired]
    public Func<TermEntry, Task> OnTermSelect { get; set; } = null!;



    private async Task UpdateTermSearchResults_UI_Async( string? termText ) {
        if( this.Disabled ) {
            return;
        }

        this.SelectedTerm = null;

        if( termText is not null ) {
            IEnumerable<TermEntry> terms = await this.Data.GetTermsByCriteria_Async(
                new ClientDataAccess.GetTermsByCriteriaParams( termText, null )
            );
            this.SearchOptions = terms.ToList();    // TODO
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

        int optionCount = this.SearchOptions.Count();
        if( optionCount == 0 ) {
            return;
        }

        switch( arg.Key ) {
            case "ArrowUp":
                this.IsCurrentInputSuppressed = optionCount > 0;
                this.SearchPosition--;
                break;
            case "ArrowDown":
                this.IsCurrentInputSuppressed = optionCount > 0;
                this.SearchPosition++;
                break;
            case "Enter":
                this.IsCurrentInputSuppressed = optionCount > 0;
                break;
        }

        if( this.SearchPosition < 0 ) {
            this.SearchPosition = 0;
        } else if( this.SearchPosition >= optionCount ) {
            this.SearchPosition = optionCount - 1;
        }

        if( arg.Key == "Enter" && optionCount > 0 ) {
            await this.SelectSearchResults_Async( this.SearchOptions[this.SearchPosition] );
        }

        this.SearchText = this.SearchOptions[ this.SearchPosition ]?.ToString() ?? "";
    }


    private async Task SelectSearchResults_UI_Async( TermEntry term ) {
        if( this.Disabled ) {
            return;
        }
        if( !this.IsSeachFocused ) {
            return;
        }

        await this.SelectSearchResults_Async( term );
    }

    private async Task SelectSearchResults_Async( TermEntry term ) {
        this.SelectedTerm = term;

        this.SearchText = term.ToString() ?? "";

        await this.OnTermSelect.Invoke( term );

       // await this.Js.InvokeVoidAsync(
       //     "window.TermInputComponent.SetTermSearchResult",
       //     new object[] { this.InputElement, term.ToString() ?? "" }
       //);
    }
}
