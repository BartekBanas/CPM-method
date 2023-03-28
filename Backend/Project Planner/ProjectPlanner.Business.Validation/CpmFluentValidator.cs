using FluentValidation;
using ProjectPlanner.Infrastructure.TaskObjects;

namespace ProjectPlanner.Business.Validation;

public class CpmFluentValidator : AbstractValidator<CpmTask>
{
    public string? ErrorMessage { get; private set; }
    
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
    
    
    // private bool ValidateSequences(CpmTask task, ValidationContext<CpmTask> validationContext)
    // {
    //     foreach (var activity in task.Activities)
    //     {
    //         if (activity.Sequence.Length != 2)
    //         {
    //             int index = task.Activities.IndexOf(activity) + 1;
    //             AddToErrorMessage("Activity " + index +  " is incomplete");
    //             
    //             return false;
    //         }
    //
    //         if (activity.Sequence[0] == activity.Sequence[1])
    //         {
    //             int index = task.Activities.IndexOf(activity) + 1;
    //             AddToErrorMessage("Activity " + index + " cannot come in between one and the same event");
    //             
    //             return false;
    //         }
    //     }
    //
    //     return true;
    // }

}