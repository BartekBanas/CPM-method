namespace ProjectPlanner.Application.Services.Abstractions;

public interface ITaskHandler
{
    public Task<Solution> Handle(TaskRequest taskRequest);
}