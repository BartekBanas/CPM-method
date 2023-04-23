namespace ProjectPlanner.Business.CriticalPathMethod;

public class CpmEvent
{
    public int Id { get; set; }
    public int EarliestTime { get; set; }
    public int LatestTime { get; set; }
    public int Slack { get; set; }

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