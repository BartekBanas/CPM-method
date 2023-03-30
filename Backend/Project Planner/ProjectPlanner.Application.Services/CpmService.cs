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
        throw new NotImplementedException();
        
        // Creating object from business layer
    }

}

