namespace ProjectPlanner.Business.TransportProblem.Dtos;

public class TpTask
{
    public Task<TpSolution> Solve(TpTask task)
    {
        TpProject cpmProject = new TpProject(task);

        return Task.FromResult(cpmProject.CreateSolution());
    }
}