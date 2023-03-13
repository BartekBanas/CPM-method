using ProjectPlanner.Application.Services.Abstractions;
using ProjectPlanner.Infrastructure.SolutionObjects.Abstractions;

namespace ProjectPlanner.Application.Services;

public class CpmHandler : ITaskHandler
{
    public Task<Solution> Handle(TaskRequest taskRequest)
    {
        throw new NotImplementedException();
    }
}

