using ProjectPlanner.Business.CriticalPathMethod.Dtos;

namespace ProjectPlanner.Business.CriticalPathMethod;

public class CpmProject
{
    public CpmSolution CreateSolution(CpmTask task)
    {
        SetupActivities(task);
        
        var activities = task.Activities;

        // Calculate earliest and latest start times for each activity
        var earliestStartTimes = new Dictionary<int, int>();
        var latestStartTimes = new Dictionary<int, int>();
        var eventDictionary = new Dictionary<int, CpmEvent>();

        int earliestStart = 0;
        foreach (var activity in activities)
        {
            int id = activity.Id;
            earliestStartTimes[id] = earliestStart;

            int latestStart = int.MaxValue;
            foreach (var otherActivity in activities)
            {
                if (activity.Sequence[1] == otherActivity.Sequence[0])
                {
                    latestStart = Math.Min(latestStart, earliestStartTimes[otherActivity.Id] - otherActivity.Duration);
                }
            }

            latestStartTimes[id] = latestStart;
            earliestStart = earliestStartTimes[id] + activity.Duration;

            eventDictionary[id] = new CpmEvent
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
        foreach (var activity in activities)
        {
            if (eventDictionary[activity.Id].Slack == 0)
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
            Activities = activities,
            Events = eventDictionary.Values.ToList()
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
}