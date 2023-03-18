using ProjectPlanner.Infrastructure.TaskObjects;

namespace ProjectPlanner.Business.Validation;

public class CpmTaskValidator
{
    public CpmTask Task { get; }
    public bool Validity { get; set; }

    public String ErrorMessage { get; set; }

    public CpmTaskValidator(CpmTask task)
    {
        Task = task;
        Validity = true;
    }

    public bool Validate()
    {
        ValidateAmountOfActivities();
        ValidateSequences();
        ValidateStartAndEnd();
        
        return Validity;
    }

    private bool ValidateAmountOfActivities()
    {
        if (Task.Activities.Count < 2)
        {
            Validity = false;

            ErrorMessage += "Not enough given activities\n";
            
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

    private void ValidateStartAndEnd()
    {
        int startCount = 0;
        int endCount = 0;

        HashSet<int> events = new HashSet<int>();
        
        foreach (var activity in Task.Activities)
        {
            events.Add(activity.Sequence[0]);
            events.Add(activity.Sequence[1]);
        }

        foreach (var eEvent in events)
        {
            int predecessors = 0;
            int successors = 0;
            
            foreach (var activity in Task.Activities)
            {
                if (eEvent == activity.Sequence[0])
                {
                    successors++;
                }
                if (eEvent == activity.Sequence[1])
                {
                    predecessors++;
                }
            }

            if (predecessors == 0)
                startCount++;

            if (successors == 0)
                endCount++;
        }

        if (startCount != 1 || endCount != 1)
        {
            Validity = false;
        }
    }
}