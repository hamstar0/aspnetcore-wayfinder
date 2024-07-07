using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Wayfinder.Shared.Utility.Timeline.Data;


namespace Wayfinder.Shared.Utility;


public partial class TimelineEntry {
	public long Id { get; private set; }

	[JsonIgnore]
	public bool IsAssignedId { get; private set; } = false;


	public IReadOnlyList<TimelineEventEntry> Events => this._Events.AsReadOnly();

    private IList<TimelineEventEntry> _Events = new List<TimelineEventEntry>();

	private IDictionary<long, int> _EventsByIds = new Dictionary<long, int>();



    public TimelineEntry( IEnumerable<TimelineEventEntry> events ) {
		this._Events = events.ToList();

		foreach( TimelineEventEntry evt in events ) {
			this.AddEvent( evt );
		}
    }

    public TimelineEntry( long id, IEnumerable<TimelineEventEntry> events ) {
		this.Id = id;
		this.IsAssignedId = true;

		this._Events = events.ToList();

		foreach( TimelineEventEntry evt in events ) {
			this.AddEvent( evt );
		}
    }


    public bool Equals( TimelineEntry? other ) {
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


	public void Clear() {
		this._Events.Clear();
		this._EventsByIds.Clear();
    }

	public void AddEvent( TimelineEventEntry evt ) {
		for( int i = 0; i < this.Events.Count; i++ ) {
			if( evt.EndTime >= this.Events[i].StartTime ) {
				continue;
			}

			while( this._EventsByIds.ContainsKey(evt.Id) ) {
				int collideeIdx = this._EventsByIds[evt.Id];
                TimelineEventEntry collidee = this.Events[collideeIdx];

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


	public IList<TimelineEventEntry> GetEventsBetween( DateTime start, DateTime end ) {
		var evts = new List<TimelineEventEntry>();

		foreach( TimelineEventEntry evt in this.Events ) {
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
		TimelineEventEntry collidee = this.Events[ idx ];

		this._EventsByIds.Remove( collidee.Id );

		do {
			collidee.GetNewAutoId();
		} while( this._EventsByIds.ContainsKey(collidee.Id) || alsoAvoid.Contains(collidee.Id) );

		this._EventsByIds[ collidee.Id ] = idx;
	}


	public bool ContainsTimeline( TimelineEntry other ) {
		return this.ContainsTimeline( other, (t1, t2) => t1.Equals(t2) );
	}

	public bool ContainsTimeline(
				TimelineEntry other,
				Func<ITimelineDataEntry, ITimelineDataEntry, bool> validateDataContain ) {
		if( other.Events.Count == 0 ) {
			return true;
		}

		LinkedList<TimelineEventEntry> otherList = new LinkedList<TimelineEventEntry>( other.Events );
        LinkedListNode<TimelineEventEntry>? otherNode;

        for( int i=0; i<this._Events.Count; i++ ) {
            otherNode = otherList.First;

            if( this._Events[i].StartTime > otherNode!.Value.StartTime ) {
				return false;
            }

			do {
				// Test seg exceeds current tester seg. Go to next test seg.
				if( this._Events[i].EndTime < otherNode!.Value.EndTime ) {
                    otherNode = otherNode.Next;
                    if( otherNode is null ) {
                        break;
                    } else {
						continue;
					}
                }

				// Test seg is accounted for. Go to next test seg.
				if( validateDataContain(this._Events[i].Data, otherNode!.Value.Data) ) {
					var goneNode = otherNode;
                    otherNode = otherNode.Next;

                    otherList.Remove( goneNode );
                } else {
                    otherNode = otherNode.Next;
                }

				// No more segs to test. Go to next tester seg.
                if( otherNode is null ) {
					break;
                }
            } while( this._Events[i].StartTime <= otherNode!.Value.StartTime );
        }

		return otherList.First is null;
	}
}
