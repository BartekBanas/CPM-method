using ProjectPlanner.Application.Services.Abstractions;
using ProjectPlanner.Infrastructure.TaskObjects;

namespace ProjectPlanner.Application.Services.Requests;

public class CpmRequest : TaskRequest
{
    public CpmTask CpmTask { get; set; }
    
    public CpmRequest(CpmTask cpmTask)
    {
        CpmTask = cpmTask;
    }
}