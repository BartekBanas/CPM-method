namespace ProjectPlanner.Business.CriticalPathMethod;

public class CpmEvent
{
    public int Id { get; set; }
    public int EarliestTime { get; set; }
    public int LatestTime { get; set; }
    public int Slack { get; set; }
    
    public HashSet<int> Predecessors { get; set; }
    public HashSet<int> Successors { get; set; }

    public CpmEvent(int id, int earliestTime, int latestTime)
    {
        Id = id;
        EarliestTime = earliestTime;
        LatestTime = latestTime;
    }

    public CpmEvent()
    {
    }

    public CpmEvent(int eventId)
    {
        Id = eventId;
    }
}