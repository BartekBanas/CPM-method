using ProjectPlanner.Business.CriticalPathMethod.Dtos;

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
        return project.CreateSolution();
    }
}