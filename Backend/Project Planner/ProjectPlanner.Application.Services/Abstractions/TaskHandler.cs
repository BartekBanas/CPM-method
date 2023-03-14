using ProjectPlanner.Infrastructure.SolutionObjects.Abstractions;
using ProjectPlanner.Infrastructure.TaskObjects.Abstractions;

namespace ProjectPlanner.Application.Services.Abstractions;

public interface IService
{
    public Task<Solution> Solve(ProjectTask task);
}