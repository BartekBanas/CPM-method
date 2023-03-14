using ProjectPlanner.Infrastructure.TaskObjects.Abstractions;

namespace ProjectPlanner.Infrastructure.TaskObjects;

public class CpmTask : ProjectTask
{
    public List<CpmActivity> Activities { get; set; } = null!;
}