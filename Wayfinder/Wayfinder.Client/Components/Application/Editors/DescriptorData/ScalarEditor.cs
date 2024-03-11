using System;
using Microsoft.AspNetCore.Components;


namespace Wayfinder.Client.Components.Application.Editors.DescriptorData;


public partial class ScalarEditor {
    public static double? ParseRaw( object? obj ) {
        return obj is null
            ? null
            : double.TryParse( obj.ToString(), out double d )
                ? d
                : null;
    }


    //[Inject]
    //public IJSRuntime Js { get; set; } = null!;

    //[Inject]
    //public ClientDataAccess Data { get; set; } = null!;


    [Parameter]
    public bool Disabled { get; set; } = false;

    [Parameter]
    public string Label { get; set; } = "Enter value";

    [Parameter]
    public double Value { get; set; }

    [Parameter]
    public IList<(double Min, double Max)> Ranges { get; set; } = new List<(double, double)>();

    [Parameter, EditorRequired]
    public Func<double, Task> OnInput { get; set; } = null!;


    private async Task OnMyInput_UI_Async( double value ) {
        if( this.Disabled ) { return; }

        (double min, double max)? range = this.FindNearestRange( value );
        if( range is not null ) {
            if( value < range.Value.min ) {
                value = range.Value.min;
            } else if( value > range.Value.max ) {
                value = range.Value.max;
            }
        }

        this.Value = value;

        await this.OnInput( value );
    }

    private (double Min, double Max)? FindNearestRange( double value) {
        if( this.Ranges.Count == 0 ) {
            return null;
        }

        int closestMinIdx = 0;
        int closestMaxIdx = 0;

        // TODO: Improve algorithm
        for( int i=0; i<this.Ranges.Count; i++ ) {
            (double min, double max) = this.Ranges[i];

            if( value >= min && (value - min) < this.Ranges[closestMinIdx].Min ) {
                closestMinIdx = i;
            }
            if( value <= max && (max - value) < this.Ranges[closestMaxIdx].Max ) {
                closestMaxIdx = i;
            }
        }

        int closestRangeIdx = -1;

        if( closestMinIdx == closestMaxIdx ) {
            closestRangeIdx = closestMinIdx;
        } else {
            double closestMinDiff = this.Ranges[closestMinIdx].Min - value;
            double closestMaxDiff = Math.Abs(this.Ranges[closestMaxIdx].Max - value);

            if( closestMinDiff < closestMaxDiff ) {
                closestRangeIdx = closestMinIdx;
            }
            else {
                closestRangeIdx = closestMaxIdx;
            }
        }

        return this.Ranges[closestRangeIdx];
    }
}
