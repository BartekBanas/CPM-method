using ProjectPlanner.Infrastructure.TaskObjects;

namespace ProjectPlanner.Business.Validation;

public class CpmCyclicValidator
{
    public CpmTask Task { get; }
    
    public CpmCyclicValidator(CpmTask task)
    {
        Task = task;
    }
}