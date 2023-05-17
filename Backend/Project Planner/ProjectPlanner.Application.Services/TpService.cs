using FluentValidation;
using FluentValidation.Results;
using ProjectPlanner.Business.TransportationProblem;

namespace ProjectPlanner.Application.Services;

public interface ITpService
{
    Task<TpSolution> Solve(TpTask task);
    public Task<ValidationResult> Validate(TpTask task);
}

public class TpService : ITpService
{
    private readonly IValidator<TpTask> _validator;

    public TpService(IValidator<TpTask> validator)
    {
        _validator = validator;
    }
    
    public async Task<ValidationResult> Validate(TpTask task)
    {
        var validationResult = await _validator.ValidateAsync(task);

        return validationResult;
    }

    public async Task<TpSolution> Solve(TpTask task)
    {
        TpProject tpProject = new TpProject(task);

        return await Task.FromResult(tpProject.CreateSolution());
    }
}