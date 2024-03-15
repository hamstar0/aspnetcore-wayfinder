using System.Collections;
using System.Text.Json.Serialization;


namespace Wayfinder.Shared.Libraries;


public partial class Timeline<T, U> where T : TimelineEvent<U> {
	public long Id { get; set; }

	[JsonIgnore]
	public bool IsAssignedId { get; private set; } = false;


	public IReadOnlyList<T> Events => this._Events.AsReadOnly();

    private IList<T> _Events = new List<T>();

	private IDictionary<long, int> _EventsByIds = new Dictionary<long, int>();


    public Timeline() {
        this._Events = new List<T>();
    }

    public Timeline( IEnumerable<T> events ) {
		this._Events = events.ToList();

		foreach( T evt in events ) {
			this.AddEvent( evt );
		}
    }

    public Timeline( long id, IEnumerable<T> events ) {
		this.Id = id;
		this.IsAssignedId = true;

		this._Events = events.ToList();

		foreach( T evt in events ) {
			this.AddEvent( evt );
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

	public void AddEvent( T evt ) {
		for( int i = 0; i < this.Events.Count; i++ ) {
			if( evt.EndTime >= this.Events[i].StartTime ) {
				continue;
			}

			while( this._EventsByIds.ContainsKey(evt.Id) ) {
				int collideeIdx = this._EventsByIds[evt.Id];
				T collidee = this.Events[collideeIdx];

				if( collidee.IsAssignedId ) {
					if( evt.IsAssignedId ) {
						throw new Exception( "Id collision" );
					}
					evt.GetNewAutoId();
				} else if( evt.IsAssignedId ) {
					this.ReIndexEventAt( collideeIdx, new HashSet<long> { evt.Id } );
				}
			}

			this._Events.Insert( i, evt );
			this._EventsByIds[ evt.Id ] = i;

			return;
		}
	}

	public void RemoveEventById( long id ) {
		this._Events.RemoveAt( this._EventsByIds[id] );
		this._EventsByIds.Remove( id );
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


	private void ReIndexEventAt( int idx, ISet<long> alsoAvoid ) {
		T collidee = this.Events[ idx ];

		this._EventsByIds.Remove( collidee.Id );

		do {
			collidee.GetNewAutoId();
		} while( this._EventsByIds.ContainsKey(collidee.Id) || alsoAvoid.Contains(collidee.Id) );

		this._EventsByIds[ collidee.Id ] = idx;
	}
}
