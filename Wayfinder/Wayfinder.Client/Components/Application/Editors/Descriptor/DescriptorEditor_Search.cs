using System;
using Wayfinder.Shared.Data;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Utility;
using Wayfinder.Client.Data;


namespace Wayfinder.Client.Components.Application.Editors.Descriptor;


public partial class DescriptorEditor {
    private async Task UpdateDescriptorSearchResults_Async() {
        if( this.Facts is null ) {
            return;
        }

        this.SearchOptions = await this.Data.GetDescriptorsByCriteria_Async(
            new ClientDataAccess.GetDescriptorsByCriteriaParams(
                new Optional<DataTimelineEntry>( this.Facts ),
                new Optional<DescriptorConditionsTree>( this.Conditions )
            )
        );
    }

    private async Task SelectSearchResults_UI_Async( DescriptorEntry descriptor ) {
        if( this.Disabled ) { return; }

        this.SelectedDescriptor = descriptor;

        this.Facts = descriptor.Facts;
        this.Conditions = descriptor.Conditions;

        this.IsModified = false;

        await this.OnDescriptorSelect( this.SelectedDescriptor );
    }
}
