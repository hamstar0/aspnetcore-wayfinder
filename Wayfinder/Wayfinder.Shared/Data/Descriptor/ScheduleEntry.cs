using System.Collections;


namespace Wayfinder.Shared.Data.Entries.Descriptor;


public partial class ScheduleEntry : IEquatable<ScheduleEntry> {
    public long Id { get; private set; }

    public DescriptorEntry? For { get; private set; }   // 1:1

    public IReadOnlyList<ScheduleEventEntry> Events => this._events.AsReadOnly();

    private IList<ScheduleEventEntry> _events;

    private IDictionary<long, int> _EventsById = new Dictionary<long, int>();



    public ScheduleEntry() {
        this._events = new List<ScheduleEventEntry>();
    }

    public ScheduleEntry( long id, DescriptorEntry? @for, IList<ScheduleEventEntry> events ) {
        this.Id = id;
        this.For = @for;
        this._events = events;

        for( int i=0; i<events.Count; i++ ) {
            this._EventsById[ events[i].Id ] = i;
        }
    }

    public bool Equals( ScheduleEntry? other ) {
        if( other is null ) {
            return false;
        }
        if( this.For != other.For ) {
            return false;
        }
        if( this.Events.Count != other.Events.Count ) {
            return false;
        }
        if( this.Events != other.Events ) {
            for( int i = 0; i < this.Events.Count; i++ ) {
                if( !this.Events[i].Equals(other.Events[i]) ) {
                    return false;
                }
            }
        }
        return true;
    }
}
