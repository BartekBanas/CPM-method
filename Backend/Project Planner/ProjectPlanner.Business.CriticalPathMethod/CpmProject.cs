using ProjectPlanner.Business.CriticalPathMethod.Dtos;

namespace ProjectPlanner.Business.CriticalPathMethod;

public class CpmProject
{
    private Dictionary<int, CpmEvent> EventDictionary { get; set; }
    private List<CpmActivity> Activities { get; set; } = null!;
    public int StartId { get; set; }
    public int EndId { get; set; }

    public CpmProject()
    {
        EventDictionary = new Dictionary<int, CpmEvent>();
    }

    public CpmSolution CreateSolution(CpmTask task)
    {
        Activities = task.Activities;
        SetupActivities(task);
        SetUpEvents(task);
        FindStartAndEnd(task);
        CalculateEarliestTime(EventDictionary[EndId]);
        CalculateLatestTime(EventDictionary[StartId]);

        // Find critical path and mark critical activities
        // var criticalPath = new List<int>();
        // foreach (var activity in Activities)
        // {
        //     if (EventDictionary[activity.Id].Slack == 0)
        //     {
        //         activity.Critical = true;
        //         criticalPath.Add(activity.Id);
        //     }
        //     else
        //     {
        //         activity.Critical = false;
        //     }
        // }

        // Build solution object
        var solution = new CpmSolution
        {
            Activities = Activities,
            Events = EventDictionary.Values.ToList()
        };

        return solution;
    }

    private void SetupActivities(CpmTask task)
    {
        for (int i = 0; i < task.Activities.Count; i++)
        {
            task.Activities[i].Id = i;
        }
    }

    private void SetUpEvents(CpmTask task)
    {
        int eventIndex = 0;
        
        foreach (var activity in task.Activities)
        {
            if(!EventDictionary.TryGetValue(activity.Sequence[0], out var Name))
            {
                EventDictionary.Add(eventIndex, new CpmEvent(eventIndex));
                eventIndex++;
            }
            
            if(!EventDictionary.TryGetValue(activity.Sequence[1], out var whatever))
            {
                EventDictionary.Add(eventIndex, new CpmEvent(eventIndex));
                eventIndex++;
            }
        }
    }

    private void FindStartAndEnd(CpmTask task)
    {
        HashSet<int> events = new HashSet<int>();
        
        foreach (var activity in task.Activities)
        {
            events.Add(activity.Sequence[0]);
            events.Add(activity.Sequence[1]);
        }

        foreach (var eventId in events)
        {
            int predecessors = 0;
            int successors = 0;
            
            foreach (var activity in task.Activities)
            {
                if (eventId == activity.Sequence[0])
                {
                    EventDictionary[eventId].Successors.Add(activity.Sequence[1]);
                    successors++;
                }
                if (eventId == activity.Sequence[1])
                {
                    EventDictionary[eventId].Predecessors.Add(activity.Sequence[0]);
                    predecessors++;
                }
            }

            if (predecessors == 0)
            {
                StartId = eventId;
            }

            if (successors == 0)
            {
                EndId = eventId;
            }
        }
    }

    private int CalculateEarliestTime(CpmEvent cpmEvent)
    {
        int earlyTime = 0;
            
        //foreach preceding activity
        for (int i = 0; i < Activities.Count; i++)
        {
            if(Activities[i].Sequence[1] == cpmEvent.Id)
            {
                int predecessorStart = CalculateEarliestTime(EventDictionary[Activities[i].Sequence[0]]);

                if (earlyTime < predecessorStart + Activities[i].Duration)
                {
                    earlyTime = predecessorStart + Activities[i].Duration;
                }
            }
        }

        cpmEvent.EarliestTime = earlyTime;
        
        return earlyTime;
    }

    private int CalculateLatestTime(CpmEvent cpmEvent)
    {
        int lateTime = EventDictionary[EndId].EarliestTime;
            
        //foreach preceding activity
        for (int i = 0; i < Activities.Count; i++)
        {
            if(Activities[i].Sequence[0] == cpmEvent.Id)
            {
                int predecessorFinish = CalculateLatestTime(EventDictionary[Activities[i].Sequence[1]]);

                if (lateTime < predecessorFinish + Activities[i].Duration)
                {
                    lateTime = predecessorFinish - Activities[i].Duration;
                }
            }
        }

        cpmEvent.LatestTime = lateTime;
        
        return lateTime;
    }
}