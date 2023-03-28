using FluentValidation;
using ProjectPlanner.Infrastructure.TaskObjects;

namespace ProjectPlanner.Business.Validation;

public class CpmFluentValidator : AbstractValidator<CpmTask>
{
    public CpmFluentValidator()
    {
        RuleFor(task => task.Activities.Count).GreaterThan(1).WithMessage("Not enough given activities");
        RuleFor(task => task.Activities).Custom(ValidateActivities);
    }

    private void ValidateActivities(List<CpmActivity> activities, ValidationContext<CpmTask> context)
    {
        for (int i = 0; i < activities.Count; i++)
        {
            if (activities[i].Sequence.Length != 2)
            {
                context.AddFailure("Activity " + (i + 1) + " is incomplete");
            }
            
            if (activities[i].Sequence[0] == activities[i].Sequence[1])
            {
                context.AddFailure("Activity " + (i + 1) + " cannot come in between one and the same event");
            }
        }
    }
    

}