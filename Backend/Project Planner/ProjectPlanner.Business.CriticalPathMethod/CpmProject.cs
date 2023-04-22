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
        CalculateLateStart(EventDictionary[EndId]);
        CalculateEarlyStart(EventDictionary[EndId]);
        CalculateLateFinish(EventDictionary[StartId]);
        CalculateEarlyFinish(EventDictionary[StartId]);

        var temCpmSolution = new CpmSolution
        {
            Activities = Activities,
            Events = EventDictionary.Values.ToList()
        };
        
        return temCpmSolution;

        
        // Calculate earliest and latest start times for each activity
        var earliestStartTimes = new Dictionary<int, int>();
        var latestStartTimes = new Dictionary<int, int>();

        foreach (var cpmEvent in EventDictionary)
        {
            int earlyStart = 0;
            
            for (int i = 0; i < Activities.Count && Activities[i].Sequence[1] == cpmEvent.Key; i++)
            {
                if (EventDictionary[Activities[i].Sequence[0]].EarliestStart + Activities[i].Duration > earlyStart)
                {
                    
                }
            }
        }
        
        int earliestStart = 0;
        foreach (var activity in Activities)
        {
            int id = activity.Id;
            earliestStartTimes[id] = earliestStart;

            int latestStart = int.MaxValue;
            foreach (var otherActivity in Activities)
            {
                if (activity.Sequence[1] == otherActivity.Sequence[0])
                {
                    latestStart = Math.Min(latestStart, earliestStartTimes[otherActivity.Id] - otherActivity.Duration);
                }
            }

            latestStartTimes[id] = latestStart;
            earliestStart = earliestStartTimes[id] + activity.Duration;

            EventDictionary[id] = new CpmEvent
            {
                Id = id,
                EarliestStart = earliestStartTimes[id],
                LatestStart = latestStartTimes[id],
                EarliestFinish = earliestStartTimes[id] + activity.Duration,
                LatestFinish = latestStartTimes[id] + activity.Duration,
                Slack = latestStartTimes[id] - earliestStartTimes[id]
            };
        }

        // Find critical path and mark critical activities
        var criticalPath = new List<int>();
        foreach (var activity in Activities)
        {
            if (EventDictionary[activity.Id].Slack == 0)
            {
                activity.Critical = true;
                criticalPath.Add(activity.Id);
            }
            else
            {
                activity.Critical = false;
            }
        }

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

        foreach (var scopedEvent in events)
        {
            int predecessors = 0;
            int successors = 0;
            
            foreach (var activity in task.Activities)
            {
                if (scopedEvent == activity.Sequence[0])
                {
                    successors++;
                }
                if (scopedEvent == activity.Sequence[1])
                {
                    predecessors++;
                }
            }

            if (predecessors == 0)
            {
                StartId = scopedEvent;
            }

            if (successors == 0)
            {
                EndId = scopedEvent;
            }
        }
    }

    private int CalculateLateStart(CpmEvent cpmEvent)
    {
        int lateStart = 0;
            
        //foreach preceding activity
        for (int i = 0; i < Activities.Count; i++)
        {
            if(Activities[i].Sequence[1] == cpmEvent.Id)
            {
                int predecessorStart = CalculateLateStart(EventDictionary[Activities[i].Sequence[0]]);

                if (lateStart < predecessorStart + Activities[i].Duration)
                {
                    lateStart = predecessorStart + Activities[i].Duration;
                }
            }
        }

        cpmEvent.LatestStart = lateStart;
        
        return lateStart;
    }
    
    private int CalculateEarlyStart(CpmEvent cpmEvent)
    {
        if (cpmEvent.EarliestStart != 0)
        {
            return cpmEvent.EarliestStart;
        }
        
        int earlyStart = 0;
            
        //foreach preceding activity
        for (int i = 0; i < Activities.Count; i++)
        {
            if(Activities[i].Sequence[1] == cpmEvent.Id)
            {
                int predecessorStart = CalculateEarlyStart(EventDictionary[Activities[i].Sequence[0]]);

                if (earlyStart > predecessorStart + Activities[i].Duration || cpmEvent.EarliestStart == earlyStart)
                {
                    earlyStart = predecessorStart + Activities[i].Duration;
                }
            }
        }

        cpmEvent.EarliestStart = earlyStart;
        
        return earlyStart;
    }
    
    private int CalculateLateFinish(CpmEvent cpmEvent)
    {
        int lateFinish = EventDictionary[EndId].LatestStart;
            
        //foreach preceding activity
        for (int i = 0; i < Activities.Count; i++)
        {
            if(Activities[i].Sequence[0] == cpmEvent.Id)
            {
                int predecessorFinish = CalculateLateFinish(EventDictionary[Activities[i].Sequence[1]]);

                if (lateFinish < predecessorFinish + Activities[i].Duration)
                {
                    lateFinish = predecessorFinish - Activities[i].Duration;
                }
            }
        }

        cpmEvent.LatestFinish = lateFinish;
        
        return lateFinish;
    }
    
    private int CalculateEarlyFinish(CpmEvent cpmEvent)
    {
        if (cpmEvent.EarliestFinish != EventDictionary[EndId].EarliestStart)
        {
            return cpmEvent.EarliestFinish;
        }
        
        int earlyFinish = EventDictionary[EndId].EarliestStart;
            
        //foreach preceding activity
        for (int i = 0; i < Activities.Count; i++)
        {
            if(Activities[i].Sequence[0] == cpmEvent.Id)
            {
                int predecessorFinish = CalculateEarlyFinish(EventDictionary[Activities[i].Sequence[1]]);

                if (earlyFinish < predecessorFinish + Activities[i].Duration || cpmEvent.EarliestStart == earlyFinish)
                {
                    earlyFinish = predecessorFinish - Activities[i].Duration;
                }
            }
        }

        cpmEvent.EarliestFinish = earlyFinish;
        
        return earlyFinish;
    }
}