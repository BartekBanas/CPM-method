namespace ProjectPlanner.Business.CriticalPathMethod.Dtos;

public class CpmSolution
{
    public List<CpmActivity> Activities { get; set; } = new List<CpmActivity>();
    public List<CpmEvent> Events { get; set; } = new List<CpmEvent>();
}