namespace ProjectPlanner.Infrastructure.TaskObjects;

public class CpmActivity
{
    public int TaskID { get; set; }
    public string TaskName { get; set; }
    public int Duration { get; set; }
    public int[] Sequence { get; set; }
    public bool IsCritical { get; set; }

    public CpmActivity(string taskName, int duration, int[] sequence)
    {
        TaskName = taskName;
        Duration = duration;
        Sequence = sequence;
    }
}