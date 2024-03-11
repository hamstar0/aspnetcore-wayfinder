using System.ComponentModel.DataAnnotations;


namespace Wayfinder.Shared.Data.Entries.Descriptor;


public class ScheduleEventEntry : IEquatable<ScheduleEventEntry> {
    public long Id { get; set; }

    public ScheduleEntry For { get; set; } = null!; // 1:1

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public DescriptorDataEntry? State { get; set; } // use multiple ScheduleEventEntrys as needed



    public bool Equals( ScheduleEventEntry? other ) {
        if( other is null ) { return false; }
        if( this.For != other.For ) { return false; }
        if( this.StartTime != other.StartTime ) { return false; }
        if( this.EndTime != other.EndTime ) { return false; }
        if( this.State is not null ) {
            if( !this.State.Equals(other.State) ) { return false; }
        }
        return true;
    }
}
