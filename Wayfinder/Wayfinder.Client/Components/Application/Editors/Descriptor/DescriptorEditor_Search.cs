using System;
using Wayfinder.Client.Data;
using Wayfinder.Shared.Data.Entries.Descriptor;


namespace Wayfinder.Client.Components.Application.Editors.Descriptor;


public partial class DescriptorEditor {
    private async Task UpdateDescriptorSearchResults_Async() {
        if( this.TermSubj is null || this.TermRel is null ) {
            return;
        }

        this.SearchOptions = await this.Data.GetDescriptorsByCriteria_Async(
            new ClientDataAccess.GetDescriptorsByCriteriaParams( this.TermSubj, this.TermRel )
        );
    }

    private async Task SelectSearchResults_UI_Async( DescriptorEntry descriptor ) {
        if( this.Disabled ) { return; }

        this.SelectedDescriptor = descriptor;

        this.TermSubj = descriptor.TermSubj;
        this.TermRel = descriptor.TermRel;
        this.Schedule = descriptor.Schedule;
        this.Conditions = descriptor.Conditions;

        this.IsModified = false;

        await this.OnDescriptorSelect( this.SelectedDescriptor );
    }
}
