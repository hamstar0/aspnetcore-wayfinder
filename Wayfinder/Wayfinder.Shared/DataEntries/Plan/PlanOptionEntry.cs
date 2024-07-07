using System;
using System.Text.Json.Serialization;
using Wayfinder.Shared.DataEntries.Descriptor;
using Wayfinder.Shared.Utility.Timeline.Data;


namespace Wayfinder.Shared.DataEntries.Plan;


public class PlanOptionEntry : TimelineEventDataEntry
{
    //[Key]
    public long Id { get; private set; }

    [JsonIgnore]
    public bool IsAssignedId { get; private set; } = false;

    public string? Name;

    public long MinimumEnactingDuration;

    public DescriptorFactsEntry Actions;

    public DescriptorConditionsTreeEntry Conditions;



    public PlanOptionEntry(
                string? name,
                long minimumEnactingDuration,
                DescriptorFactsEntry actions,
                DescriptorConditionsTreeEntry conditions)
    {
        Name = name;
        MinimumEnactingDuration = minimumEnactingDuration;
        Actions = actions;
        Conditions = conditions;
    }

    public PlanOptionEntry(
                long id,
                string? name,
                long minimumEnactingDuration,
                DescriptorFactsEntry actions,
                DescriptorConditionsTreeEntry conditions)
    {
        Id = id;
        IsAssignedId = true;
        Name = name;
        MinimumEnactingDuration = minimumEnactingDuration;
        Actions = actions;
        Conditions = conditions;
    }

    public bool Contains(TimelineEventDataEntry data)
    {
        throw new NotImplementedException();
    }
}