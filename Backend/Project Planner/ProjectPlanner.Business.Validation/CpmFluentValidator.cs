using FluentValidation;
using ProjectPlanner.Business.CriticalPathMethod;
using ProjectPlanner.Business.CriticalPathMethod.Dtos;

namespace ProjectPlanner.Business.Validation;

public class CpmFluentValidator : AbstractValidator<CpmTask>
{
    public CpmFluentValidator()
    {
        RuleFor(task => task.Activities).Custom(ValidateActivities);
        RuleFor(task => task.Activities).Custom(ValidateStartAndEnd);
        RuleFor(task => task).Custom(ValidatePeriodicity);
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

            for (int j = 0; j < activities.Count; j++)
            {
                if (activities[i].Sequence[0] == activities[j].Sequence[0] &&
                    activities[i].Sequence[1] == activities[j].Sequence[1] && i < j)
                {
                    context.AddFailure("Activities " + (i + 1) + " and " + (j + 1) + " are duplicates");
                }
            }
        }
    }
    
    private void ValidateStartAndEnd(List<CpmActivity> activities, ValidationContext<CpmTask> context)
    {
        int startCount = 0;
        int endCount = 0;

        HashSet<int> events = new HashSet<int>();
        
        foreach (var activity in activities)
        {
            events.Add(activity.Sequence[0]);
            events.Add(activity.Sequence[1]);
        }

        foreach (var eEvent in events)
        {
            int predecessors = 0;
            int successors = 0;
            
            foreach (var activity in activities)
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
            context.AddFailure("Could not find starting event");
        }
        if (startCount > 1)
        {
            context.AddFailure("More than one starting event found");
        }
        if (endCount == 0)
        {
            context.AddFailure("Could not find ending event");
        }
        if (endCount > 1)
        {
            context.AddFailure("More than one ending event found");
        }
    }

    private void ValidatePeriodicity(CpmTask task, ValidationContext<CpmTask> context)
    {
        CpmCyclicValidator cyclicValidator = new CpmCyclicValidator(task);

        if (cyclicValidator.Validate() == false)
        {
            context.AddFailure("A cyclic dependency between tasks has been detected");
        }
    }
}