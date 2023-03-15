using ProjectPlanner.Infrastructure.TaskObjects;

namespace ProjectPlanner.Business.Validation;

public class CpmTaskValidator
{
    public CpmTask Task { get; }
    public bool Validity { get; set; }

    public CpmTaskValidator(CpmTask task)
    {
        Task = task;
        Validity = true;
    }

    public bool Validate()
    {
        return Validity;
    }

    public bool ValidateAmountOfActivities()
    {
        if (Task.Activities.Count < 2)
        {
            Validity = false;
            return false;
        }
        else
        {
            return true;
        }
    }
}