using FluentValidation;
using ProjectPlanner.Business.TransportationProblem;
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

        RuleFor(tpTask => tpTask.Suppliers).Custom(ValidateSuppliers);
        RuleFor(task => task.Recipients).Custom(ValidateRecipients);
    }

    private void ValidateSuppliers(Supplier[] suppliers ,ValidationContext<TpTask> context)
    {
        for (int i = 0; i < suppliers.Length; i++)
        {
            if (suppliers[i].Supply <= 0)
            {
                context.AddFailure("Supplier " + (i + 1) + " needs to have supply");
            }

            if (suppliers[i].Cost <= 0)
            {
                context.AddFailure("Supplier " + (i + 1) + " needs to have positive selling cost");
            }
        }
    }
    
    private void ValidateRecipients(Recipient[] recipients ,ValidationContext<TpTask> context)
    {
        for (int i = 0; i < recipients.Length; i++)
        {
            if (recipients[i].Demand <= 0)
            {
                context.AddFailure("Recipient " + (i + 1) + " needs to have demand");
            }

            if (recipients[i].Cost <= 0)
            {
                context.AddFailure("Recipient " + (i + 1) + " needs to have positive buying cost");
            }
        }
    }
}