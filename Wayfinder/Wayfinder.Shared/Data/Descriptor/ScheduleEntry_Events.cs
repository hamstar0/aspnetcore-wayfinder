using System.Collections;


namespace Wayfinder.Shared.Data.Entries.Descriptor;


public partial class ScheduleEntry : IEquatable<ScheduleEntry> {
    public IList<ScheduleEventEntry> GetEventsBetween( DateTime start, DateTime end ) {
        var evts = new List<ScheduleEventEntry>();

        foreach( ScheduleEventEntry evt in this.Events ) {
            if( evt.EndTime < start ) {
                continue;
            }

            if( evt.StartTime > end ) {
                break;
            }

            evts.Add( evt );
        }

        return evts;
    }

    public void AddEvent( ScheduleEventEntry evt ) {
        for( int i=0; i<this.Events.Count; i++ ) {
            if( evt.EndTime >= this.Events[i].StartTime ) {
                continue;
            }

            this._events.Insert( i, evt );
            this._EventsById[ evt.Id ] = i;

            break;
        }
    }

    public void RemoveEvent( long eventId ) {
        this._events.RemoveAt( this._EventsById[eventId] );
        this._EventsById.Remove( eventId );
    }
}
