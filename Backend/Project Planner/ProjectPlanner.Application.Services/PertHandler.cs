using ProjectPlanner.Application.Services.Abstractions;

namespace ProjectPlanner.Application.Services;

public class PertHandler : ITaskHandler
{
    public Task<Solution> Handle(TaskRequest taskRequest)
    {
        throw new NotImplementedException();
    }
}