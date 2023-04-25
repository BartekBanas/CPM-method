using ProjectPlanner.Business.TransportationProblem;
using ProjectPlanner.Business.TransportationProblem.Dtos;

namespace ProjectPlanner.Application.Services;

public class TpService
{
    public Task<TpSolution> Solve(TpTask task)
    {
        TpProject tpProject = new TpProject(task);

        return Task.FromResult(tpProject.CreateSolution());
    }
}