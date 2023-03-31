using ProjectPlanner.Infrastructure.SolutionObjects;
using ProjectPlanner.Infrastructure.TaskObjects;

namespace ProjectPlanner.Business.CriticalPathMethod;

public class CriticalPathMethod
{
    public CpmTask task { get; set; }
    
    public CriticalPathMethod(CpmTask task)
    {
        this.task = task;
    }
    
    CpmSolution SolveProject(CpmProject project)
    {
        return project.CreateSolution(task);
    }
}