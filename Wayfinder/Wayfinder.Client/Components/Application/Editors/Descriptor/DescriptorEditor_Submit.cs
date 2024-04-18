using System;
using Wayfinder.Shared.Data;
using Wayfinder.Shared.Data.Entries;
using Wayfinder.Shared.Data.Entries.Descriptor;
using Wayfinder.Shared.Utility;
using Wayfinder.Shared.Utility.BooleanTree;
using Wayfinder.Client.Data;


namespace Wayfinder.Client.Components.Application.Editors.Descriptor;



public partial class DescriptorEditor {
    private bool CanSubmitDescriptorFields() {
        if( this.Disabled ) {
            return false;
        }
        if( !this.CanCreate && !this.CanEdit ) {
            return false;
        }
        if( this.Facts is null ) {
            return false;
        }
        if( !this.IsModified ) {
            return false;
        }
        return true;
    }

    private bool CanCreateDescriptor() {
        if( !this.CanSubmitDescriptorFields() ) {
            return false;
        }
        if( !this.CanCreate ) {
            return false;
        }
        if( this.SelectedDescriptor is not null ) {
            return false;
        }
        return true;
    }

    private bool CanEditDescriptor() {
        if( !this.CanSubmitDescriptorFields() ) {
            return false;
        }
        if( !this.CanEdit ) {
            return false;
        }
        if( this.SelectedDescriptor is null ) {
            return false;
        }
        return true;
    }


    private async Task SubmitDescriptor_UI_Async() {
        if( !this.CanSubmitDescriptorFields() ) {
            return;
        }

        bool isCreating = this.SelectedDescriptor is null;
        bool hasCreated = false;

        if( isCreating ) {
            if( this.CanCreateDescriptor() ) {
                await this.SubmitCreate_Async();

                hasCreated = true;
            }
        } else {
            if( this.CanEditDescriptor() ) {
                await this.SubmitEdit_Async();
            }
        }

        this.IsModified = false;

        if( hasCreated ) {
            await this.OnDescriptorSelect( this.SelectedDescriptor! );
        }
    }


    private async Task SubmitCreate_Async() {
        var createParams = new ClientDataAccess.CreateDescriptorParams(
            this.Facts,
            this.Conditions
        );

        if( this.OnDescriptorCreate is null ) {
            this.SelectedDescriptor = await this.Data.CreateDescriptor_Async( createParams );
        } else {
            this.SelectedDescriptor = await this.OnDescriptorCreate( createParams );
        }
    }

    private async Task SubmitEdit_Async() {
        var editParams = new ClientDataAccess.EditDescriptorParams(
            this.SelectedDescriptor!.Id,
            new Optional<DescriptorFactsEntry>( this.Facts! ),
            new Optional<DescriptorConditionsTreeEntry>( this.Conditions )
        );

        if( this.OnDescriptorEdit is null ) {
            await this.Data.EditDescriptor_Async( editParams );
        } else {
            if( this.OnDescriptorEdit is not null && !await this.OnDescriptorEdit(editParams) ) {
                await this.Data.EditDescriptor_Async( editParams );
            }
        }
    }
}
