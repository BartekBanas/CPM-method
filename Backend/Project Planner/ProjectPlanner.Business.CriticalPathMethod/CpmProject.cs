using ProjectPlanner.Infrastructure.SolutionObjects;
using ProjectPlanner.Infrastructure.TaskObjects;

namespace CriticalPathMethod;

public class CpmProject
{
    public CpmSolution CreateSolution(CpmTask task)
    {
        return new CpmSolution();
    }
}