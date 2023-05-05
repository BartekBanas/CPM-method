using ProjectPlanner.Business.TransportationProblem;

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