using FluentValidation;
using ProjectPlanner.Business.TransportationProblem.Dtos;

namespace ProjectPlanner.Business.Validation;

public class TpFluentValidator : AbstractValidator<TpTask>
{
    public TpFluentValidator()
    {
        RuleFor(tpTask => tpTask.Suppliers)
            .NotNull().WithMessage("Suppliers cannot be null")
            .Must(suppliers => suppliers.Length > 0).WithMessage("There must be at least one supplier");

        RuleFor(tpTask => tpTask.Recipients)
            .NotNull().WithMessage("Recipients cannot be null")
            .Must(recipients => recipients.Length > 0).WithMessage("There must be at least one recipient");
    }

}