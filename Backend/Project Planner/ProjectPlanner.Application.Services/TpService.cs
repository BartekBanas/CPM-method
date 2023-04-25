using ProjectPlanner.Business.TransportationProblem;
using ProjectPlanner.Business.TransportationProblem.Dtos;

namespace ProjectPlanner.Application.Services;

public interface ITpService
{
    Task<TpSolution> Solve(TpTask task);
}

public class TpService : ITpService
{
    public Task<TpSolution> Solve(TpTask task)
    {
        TpProject tpProject = new TpProject(task);

        return Task.FromResult(tpProject.CreateSolution());
    }
}