using FluentValidation;
using ProjectPlanner.Infrastructure.TaskObjects;

namespace ProjectPlanner.Business.Validation;

public class CpmFluentValidator : AbstractValidator<CpmTask>
{
    public CpmFluentValidator()
    {
        RuleFor(task => task.Activities).Custom(ValidateActivities);
    }

    private void ValidateActivities(List<CpmActivity> activities, ValidationContext<CpmTask> context)
    {
        foreach (var activity in activities)
        {
            if (activity.Sequence.Length != 2)
            {
                int index = activities.IndexOf(activity) + 1;
                context.AddFailure("Activity " + index +  " is incomplete");
                
                return;
            }

            if (activity.Sequence[0] == activity.Sequence[1])
            {
                int index = activities.IndexOf(activity) + 1;
                context.AddFailure("Activity " + index + " cannot come in between one and the same event");
                
                return;
            }
        }
    }
}