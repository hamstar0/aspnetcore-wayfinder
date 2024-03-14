using System.Collections;


namespace Wayfinder.Shared.Libraries;


public partial class Timeline<T, U> where T : TimelineEvent<U> {
    public IReadOnlyList<T> Events => this._Events.AsReadOnly();

    private IList<T> _Events;

    private IDictionary<T, int> _EventIndexes = new Dictionary<T, int>();


    public Timeline() {
        this._Events = new List<T>();
    }

    public Timeline( IEnumerable<T> events ) {
        this._Events = events.ToList();

		for( int i=0; i<this._Events.Count; i++ ) {
            this._EventIndexes[ this._Events[i] ] = i;
        }
    }

    public bool Equals( Timeline<T, U>? other ) {
        if( other is null ) {
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

	public virtual void AddEvent( T evt ) {
		for( int i = 0; i < this.Events.Count; i++ ) {
			if( evt.EndTime >= this.Events[i].StartTime ) {
				continue;
			}

			this._Events.Insert( i, evt );
			this._EventIndexes[ evt ] = i;

			break;
		}
	}

	public virtual void RemoveEvent( T evt ) {
		this._Events.RemoveAt( this._EventIndexes[evt] );
		this._EventIndexes.Remove( evt );
	}


	public IList<T> GetEventsBetween( DateTime start, DateTime end ) {
		var evts = new List<T>();

		foreach( T evt in this.Events ) {
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
}
