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
        ValidateAmountOfActivities();
        ValidateSequences();
        
        return Validity;
    }

    private bool ValidateAmountOfActivities()
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

    private void ValidateSequences()
    {
        foreach (var activity in Task.Activities)
        {
            if (activity.Sequence.Length != 2)
            {
                Validity = false;
                
                return;
            }

            if (activity.Sequence[0] == activity.Sequence[1])
            {
                Validity = false;
                
                return;
            }
        }
    }

    
}