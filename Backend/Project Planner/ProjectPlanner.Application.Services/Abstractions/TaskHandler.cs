namespace ProjectPlanner.Application.Services.Abstractions;

public interface TaskHandler
{
    public Task<Solution> Handle(TaskRequest taskRequest);
}