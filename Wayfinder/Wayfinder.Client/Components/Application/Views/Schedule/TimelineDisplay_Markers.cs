using System;
using System.Globalization;


namespace Wayfinder.Client.Components.Application.Views.Schedule;


public partial class TimelineDisplay {
    public bool CanDisplayMinuteMarkers() {
        double width = 60d / this.ZoomScale;
        return width >= 16d;//&& width < ScheduleEditor.MaxElementWidth;
    }

    public bool CanDisplayHourMarkers() {
        double width = (60d * 60d) / this.ZoomScale;
        return width >= 16d;//&& width < ScheduleEditor.MaxElementWidth;
    }

    public bool CanDisplayDayMarkers() {
        double width = (60d * 60d * 24d) / this.ZoomScale;
        return width >= 16d;//&& width < ScheduleEditor.MaxElementWidth;
    }

    public bool CanDisplayMonthMarkers() {
        double width = ((365d / 12d) * 60d * 60d * 24d) / this.ZoomScale;
        return width > 16d;//&& width < ScheduleEditor.MaxElementWidth;
    }
    
    public bool CanDisplayYearMarkers() {
        double width = (365d * 60d * 60d * 24d) / this.ZoomScale;
        return width >= 16d;//&& width < ScheduleEditor.MaxElementWidth;
    }


    public IList<(int, string)> GetTimeUnitMinuteMarkers( double maxWidth ) {
        double subSec = (double)this.ViewTimeStart.Microsecond / 1000d;
        double x = -((double)this.ViewTimeStart.Second + subSec);
        x /= this.ZoomScale;
        int minute = this.ViewTimeStart.Minute;

        var markers = new List<(int, string)>();

        while( x < maxWidth ) {
            x += 60d / this.ZoomScale;
            minute += 1;
            minute %= 60;

            markers.Add( ((int)x, minute.ToString("00")) );
        }

        return markers;
    }

    public IList<(int, string)> GetTimeUnitHourMarkers( double maxWidth ) {
        double secondsPerMinute = 60;

        double subSec = (double)this.ViewTimeStart.Microsecond / 1000d;
        double subMin = ((double)this.ViewTimeStart.Second + subSec) / 60d;
        double x = -((double)this.ViewTimeStart.Minute + subMin);
        x *= secondsPerMinute;
        x /= this.ZoomScale;
        int hour = this.ViewTimeStart.Hour;

        var markers = new List<(int, string)>();

        while( x < maxWidth ) {
            x += (60d / this.ZoomScale) * secondsPerMinute;
            hour += 1;
            hour %= 24;

            markers.Add( ((int)x, (hour + 1).ToString("00")) );
        }

        return markers;
    }

    public IList<(int, string)> GetTimeUnitDayMarkers( double maxWidth ) {
        double secondsPerHour = 60 * 60;

        double subMin = (double)this.ViewTimeStart.Second / 60d;
        double subHour = ((double)this.ViewTimeStart.Minute + subMin) / 60d;

        double x = -((double)this.ViewTimeStart.Hour + subHour);
        x *= secondsPerHour;
        x /= this.ZoomScale;

        DateTime stepTime = this.ViewTimeStart;

        var markers = new List<(int, string)>();

        while( x < maxWidth ) {
            x += (24d / this.ZoomScale) * secondsPerHour;
            stepTime = stepTime.AddDays(1);

            markers.Add( ((int)x, stepTime.Day.ToString("00")) );
        }

        return markers;
    }

    public IList<(int, string)> GetTimeUnitMonthMarkers( double maxWidth ) {
        double secondsPerDay = 60 * 60 * 24;

        double subMin = (double)this.ViewTimeStart.Second / 60d;
        double subHour = ((double)this.ViewTimeStart.Minute + subMin) / 60d;
        double subDay = ((double)this.ViewTimeStart.Hour + subHour) / 24d;

        double x = -((double)this.ViewTimeStart.Day + subDay);
        x *= secondsPerDay;
        x /= this.ZoomScale;

        DateTime stepTime = this.ViewTimeStart;

        var markers = new List<(int, string)>();

        while( x < maxWidth ) {
            stepTime = stepTime.AddMonths( 1 );

            double daysInMonth = DateTime.DaysInMonth( stepTime.Year, stepTime.Month );
            x += (daysInMonth / this.ZoomScale) * secondsPerDay;

            markers.Add( ((int)x, stepTime.ToString("MMM", CultureInfo.InvariantCulture)) );
        }

        return markers;
    }

    public IList<(int, string)> GetTimeUnitYearMarkers( double maxWidth ) {
        // pixels per month
        double SecondsPerMonth( int y, int m ) => DateTime.DaysInMonth(y, m) * 60 * 60 * 24;
        double secondsPerYear = 365 * 60 * 60 * 24;

        DateTime stepTime = this.ViewTimeStart;

        double daysPerStartMonth = SecondsPerMonth( stepTime.Year, stepTime.Month );

        double subMin = (double)stepTime.Second / 60d;
        double subHour = ((double)stepTime.Minute + subMin) / 60d;
        double subDay = ((double)stepTime.Hour + subHour) / 24d;
        double subMonth = ((double)stepTime.Month + subDay) / daysPerStartMonth;

        double x = -((double)stepTime.Month + subMonth);
        x *= SecondsPerMonth( stepTime.Year, stepTime.Month );
        x /= this.ZoomScale;

        var markers = new List<(int, string)>();

        while( x < maxWidth ) {
            stepTime = stepTime.AddYears( 1 );

            x += secondsPerYear / this.ZoomScale;

            markers.Add( ((int)x, stepTime.ToString("yyyy", CultureInfo.InvariantCulture)) );
        }

        return markers;
    }
}
