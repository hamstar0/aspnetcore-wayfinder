using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Wayfinder.Client.Data;
using Wayfinder.Shared.DataEntries;
using Wayfinder.Shared.DataEntries.Descriptor;
using Wayfinder.Shared.Utility;


namespace Wayfinder.Client.Components.Application.Editors;



public partial class GoalEditor {
    //[Inject]
    //public IJSRuntime Js { get; set; } = null!;

    [Inject]
    public ClientDataAccess Data { get; set; } = null!;


    private ElementReference InputElement;

    private bool IsSeachFocused = false;

    private string? SearchText = null;

    private GoalEntry? SelectedGoal = null;

    private IEnumerable<GoalEntry> SearchOptions = new List<GoalEntry>();


    private DescriptorEntry? Needed = null;


    [Parameter]
    public bool Disabled { get; set; } = false;

    [Parameter, EditorRequired]
    public Func<GoalEntry, Task> OnGoalSelect { get; set; } = null!;



    private async Task UpdateGoalSearchResults_UI_Async( string? text ) {
        this.SearchText = text;
        this.SelectedGoal = null;

        if( text is not null ) {
            this.SearchOptions = await this.Data.GetGoalsByCriteria_Async(
                new ClientDataAccess.GetGoalsByCriteriaParams(
                    namePattern: new Optional<string>(text)
                )
            );
        }
        else {
            this.SearchOptions = new List<GoalEntry>();
        }
    }


    private async Task InteractSearchResults_UI_Async( KeyboardEventArgs arg ) {
        if( this.Disabled ) {
            return;
        }
        if( !this.IsSeachFocused ) {
            return;
        }

        f
    }

    private async Task SubmitNewGoal_UI_Async() {
        if( this.Disabled ) {
            return;
        }
        if( this.SearchText is null || this.Needed is null ) {
            return;
        }

        GoalEntry? newGoal = await this.Data.CreateGoal_Async(
            new ClientDataAccess.CreateGoalParams(
                name: this.SearchText,
                desc: "",
                needed: this.Needed
            )
        );
        if( newGoal is null ) {
            return;
        }

        this.SelectedGoal = newGoal;

        await this.OnGoalSelect( newGoal );

        // await Js.InvokeVoidAsync(
        //     "window.GoalInputComponent.SetGoalSearchResult",
        //     new object[] { this.InputElement, newGoal.ToString() ?? "" }
        // );
    }
}
