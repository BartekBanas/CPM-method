using ProjectPlanner.Business.TransportProblem;
using ProjectPlanner.Business.TransportProblem.Dtos;

namespace ProjectPlanner.Application.Services;

public class TpService
{
    public Task<TpSolution> Solve(TpTask task)
    {
        TpProject cpmProject = new TpProject(task);

        return Task.FromResult(cpmProject.CreateSolution());
    }
}