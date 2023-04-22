using ProjectPlanner.Business.CriticalPathMethod.Dtos;

namespace ProjectPlanner.Business.CriticalPathMethod;

public class CpmProject
{
    private Dictionary<int, CpmEvent> EventDictionary { get; set; }
    private List<CpmActivity> Activities { get; set; } = null!;

    public CpmProject()
    {
        EventDictionary = new Dictionary<int, CpmEvent>();
    }

    public CpmSolution CreateSolution(CpmTask task)
    {
        Activities = task.Activities;
        SetupActivities(task);
        SetUpEvents(task);
        
        var temCpmSolution = new CpmSolution
        {
            Activities = Activities,
            Events = EventDictionary.Values.ToList()
        };

        return temCpmSolution;

        
        // Calculate earliest and latest start times for each activity
        var earliestStartTimes = new Dictionary<int, int>();
        var latestStartTimes = new Dictionary<int, int>();
        

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
            //if (EventDictionary[activity.Sequence[0]] == null)
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
}