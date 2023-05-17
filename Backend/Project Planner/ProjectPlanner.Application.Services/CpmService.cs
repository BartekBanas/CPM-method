using FluentValidation;
using FluentValidation.Results;
using ProjectPlanner.Business.CriticalPathMethod;

namespace ProjectPlanner.Application.Services;

public interface ICpmService
{
    public Task<ValidationResult> Validate(CpmTask task);
    Task<CpmSolution> Solve(CpmTask task);
}

public class CpmService : ICpmService
{
    private readonly IValidator<CpmTask> _validator;

    public CpmService(IValidator<CpmTask> validator)
    {
        _validator = validator;
    }

    public async Task<ValidationResult> Validate(CpmTask task)
    {
        var validationResult = await _validator.ValidateAsync(task);

        return validationResult;
    }

    public async Task<CpmSolution> Solve(CpmTask task)
    {
        CpmProject cpmProject = new CpmProject(task);

        return await Task.FromResult(cpmProject.CreateSolution());
    }
}