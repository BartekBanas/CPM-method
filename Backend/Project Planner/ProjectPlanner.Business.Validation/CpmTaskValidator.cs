using ProjectPlanner.Business.CriticalPathMethod.Dtos;

namespace ProjectPlanner.Business.Validation;

public class CpmTaskValidator
{
    private CpmTask Task { get; }
    private bool Validity { get; set; }
    public string? ErrorMessage { get; private set; }

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
        //ValidateLooping();
        
        return Validity;
    }

    private void AddToErrorMessage(string errorMessage)
    {
        ErrorMessage += errorMessage + "\n";
    }

    private bool ValidateAmountOfActivities()
    {
        if (Task.Activities.Count < 2)
        {
            Validity = false;

            AddToErrorMessage("Not enough given activities");

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

                int index = Task.Activities.IndexOf(activity) + 1;
                AddToErrorMessage("Activity " + index +  " is incomplete");
                
                return;
            }

            if (activity.Sequence[0] == activity.Sequence[1])
            {
                Validity = false;

                int index = Task.Activities.IndexOf(activity) + 1;
                AddToErrorMessage("Activity " + index + " cannot come in between one and the same event");
                
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

        if (startCount == 0)
        {
            AddToErrorMessage("Could not find starting event");
            Validity = false;
        }
        if (startCount > 1)
        {
            AddToErrorMessage("More than one starting event found");
            Validity = false;
        }
        if (endCount == 0)
        {
            AddToErrorMessage("Could not find ending event");
            Validity = false;
        }
        if (endCount > 1)
        {
            AddToErrorMessage("More than one ending event found");
            Validity = false;
        }
    }

    private void ValidateDuplicates()
    {
        //Note sure if needed at all
    }

    private void ValidateLooping()
    {
        var cyclicValidator = new CpmCyclicValidator(Task);

        if (cyclicValidator.Validate() == false)
        {
            Validity = false;
            AddToErrorMessage("Cycle detected");
        }
    }
}