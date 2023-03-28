using FluentValidation;
using ProjectPlanner.Infrastructure.TaskObjects;

namespace ProjectPlanner.Business.Validation;

public class CpmFluentValidator : AbstractValidator<CpmTask>
{
    public string? ErrorMessage { get; private set; }
    
    public CpmFluentValidator()
    {
        RuleFor(cpmtask => cpmtask).Must(ValidateSequences).WithMessage( $"{ErrorMessage} ErrorMessage");

    }
    
    private void AddToErrorMessage(string errorMessage)
    {
        ErrorMessage += errorMessage + "\n";
    }
    
    
    private bool ValidateSequences(CpmTask task)
    {
        foreach (var activity in task.Activities)
        {
            if (activity.Sequence.Length != 2)
            {
                int index = task.Activities.IndexOf(activity) + 1;
                AddToErrorMessage("Activity " + index +  " is incomplete");
                
                return false;
            }

            if (activity.Sequence[0] == activity.Sequence[1])
            {
                int index = task.Activities.IndexOf(activity) + 1;
                AddToErrorMessage("Activity " + index + " cannot come in between one and the same event");
                
                return false;
            }
        }

        return true;
    }

}