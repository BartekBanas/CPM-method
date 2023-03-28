using FluentValidation;
using ProjectPlanner.Infrastructure.SolutionObjects.Abstractions;
using ProjectPlanner.Infrastructure.TaskObjects;

namespace ProjectPlanner.Application.Services;

public interface ICpmService
{
    Task<Solution> Solve(CpmTask task);
}

public class CpmService : ICpmService
{
    private readonly IValidator<CpmTask> _validator;

    public CpmService(IValidator<CpmTask> validator)
    {
        _validator = validator;
    }

    public Task<Solution> Solve(CpmTask task)
    {
        throw new NotImplementedException();
        
        // Creating object from business layer
    }

}

