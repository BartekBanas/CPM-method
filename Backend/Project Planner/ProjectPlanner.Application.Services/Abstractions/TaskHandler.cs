using ProjectPlanner.Application.Services.Abstractions;
using ProjectPlanner.Infrastructure.SolutionObjects.Abstractions;

public interface ITaskHandler
{
    public Task<Solution> Handle(TaskRequest taskRequest);
}