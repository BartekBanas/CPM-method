using ProjectPlanner.Infrastructure.TaskObjects;

namespace ProjectPlanner.Business.Validation;

public class CpmTaskValidator
{
    public CpmTask Task { get; }

    public CpmTaskValidator(CpmTask task)
    {
        Task = task;
    }

    public bool Validate()
    {
        
    }

    public bool ValidateAmountOfActivities()
    {
        if (Task.Activities.Count < 2)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}