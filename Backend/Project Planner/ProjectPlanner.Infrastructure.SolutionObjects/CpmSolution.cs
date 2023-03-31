using ProjectPlanner.Infrastructure.TaskObjects;

namespace ProjectPlanner.Infrastructure.SolutionObjects;

public class CpmSolution
{
    public List<CpmActivity> Activities { get; set; } = null!;
}