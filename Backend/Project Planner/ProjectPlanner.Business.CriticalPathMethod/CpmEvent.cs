namespace ProjectPlanner.Business.CriticalPathMethod;

public class CpmEvent
{
    public int Id { get; set; }
    public int EarliestStart { get; set; }
    public int LatestStart { get; set; }
    public int EarliestFinish { get; set; }
    public int LatestFinish { get; set; }
    public int Slack { get; set; }

    public CpmEvent(int id, int earliestStart, int latestStart, int earliestFinish, int latestFinish)
    {
        Id = id;
        EarliestStart = earliestStart;
        LatestStart = latestStart;
        EarliestFinish = earliestFinish;
        LatestFinish = latestFinish;
    }

    public CpmEvent(int id, int earliestStart, int latestStart, int earliestFinish, int latestFinish, int slack)
    {
        Id = id;
        EarliestStart = earliestStart;
        LatestStart = latestStart;
        EarliestFinish = earliestFinish;
        LatestFinish = latestFinish;
        Slack = slack;
    }

    public CpmEvent()
    {
    }

    public CpmEvent(int eventId)
    {
        Id = eventId;
    }
}