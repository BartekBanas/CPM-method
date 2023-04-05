using ProjectPlanner.Business.CriticalPathMethod;
using ProjectPlanner.Business.CriticalPathMethod.Dtos;

namespace ProjectPlanner.Application.Services;

public interface ICpmService
{
    Task<CpmSolution> Solve(CpmTask task);
}

public class CpmService : ICpmService
{
    public Task<CpmSolution> Solve(CpmTask task)
    {
        CpmProject cpmProject = new CpmProject();

        return Task.FromResult(cpmProject.CreateSolution(task));
    }

}

