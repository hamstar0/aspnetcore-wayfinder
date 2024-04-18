using System;
using Wayfinder.Shared.Data;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Utility;
using Wayfinder.Client.Data;


namespace Wayfinder.Client.Components.Application.Editors.Descriptor;


public partial class DescriptorEditor {
    private async Task UpdateDescriptorSearchResults_Async() {
        DescriptorFactsEntry facts = this.Search_Facts;
        DescriptorConditionsTreeEntry conds = this.Search_Conditions;

        if( facts.Events.Count == 0 && conds.Children.Count == 0 ) {
            this.SearchResults = [];
        } else {
            this.SearchResults = await this.SearchForOptions_Async( facts, conds );
        }
    }

    private async Task<IEnumerable<DescriptorEntry>> SearchForOptions_Async(
                DescriptorFactsEntry facts,
                DescriptorConditionsTreeEntry conds ) {
        return await this.Data.GetDescriptorsByCriteria_Async(
            new ClientDataAccess.GetDescriptorsByCriteriaParams(
                new Optional<DescriptorFactsEntry>( facts ),
                new Optional<DescriptorConditionsTreeEntry>( conds )
            )
        );
    }


    private async Task SelectSearchResults_UI_Async( DescriptorEntry descriptor ) {
        if( this.Disabled ) { return; }

        this.SelectedDescriptor = descriptor;

        this.Create_Facts.Clear();
        this.Create_Conditions.Clear();

        this.Edit = descriptor;
        this.EditChanged_Facts = null;
        this.EditChanged_Conditions = null;

        await this.OnDescriptorSelect( this.SelectedDescriptor );
    }
}
