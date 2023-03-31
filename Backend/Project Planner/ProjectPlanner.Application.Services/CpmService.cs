using ProjectPlanner.Business.CriticalPathMethod;
using ProjectPlanner.Infrastructure.SolutionObjects.Abstractions;
using ProjectPlanner.Infrastructure.TaskObjects;

namespace ProjectPlanner.Application.Services;

public interface ICpmService
{
    Task<Solution> Solve(CpmTask task);
}

public class CpmService : ICpmService
{
    public Task<Solution> Solve(CpmTask task)
    {
        CpmProject cpmProject = new CpmProject();
        
        throw new NotImplementedException();
        
        // Creating object from business layer
    }

}

