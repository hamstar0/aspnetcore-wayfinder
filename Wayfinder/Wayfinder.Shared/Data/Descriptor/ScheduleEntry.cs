using System.Collections;
using Wayfinder.Shared.Libraries;


namespace Wayfinder.Shared.Data.Entries.Descriptor;


public partial class ScheduleEntry : Timeline<ScheduleEventEntry, DescriptorDataEntry> {
    public long Id { get; set; }

    //public DescriptorEntry For { get; set; }   // 1:1



    public ScheduleEntry() {
        this.Id = -1;
    }

    public ScheduleEntry( long id, IEnumerable<ScheduleEventEntry> events ) : base( events ) {
        this.Id = id;
    }


	public override void AddEvent( ScheduleEventEntry evt ) {
		base.AddEvent( evt );

        f
	}

	public override void RemoveEvent( ScheduleEventEntry evt ) {
		base.RemoveEvent( evt );

        f
	}

	public void RemoveEventById( long id ) {
        f
    }
}
