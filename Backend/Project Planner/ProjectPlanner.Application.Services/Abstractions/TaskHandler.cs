using ProjectPlanner.Infrastructure.SolutionObjects.Abstractions;

namespace ProjectPlanner.Application.Services.Abstractions;

public interface ITaskHandler
{
    public Task<Solution> Handle(TaskRequest taskRequest);
}