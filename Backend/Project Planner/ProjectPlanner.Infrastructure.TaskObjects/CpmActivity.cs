namespace ProjectPlanner.Infrastructure.TaskObjects;

public class CpmActivity
{
    public int TaskID { get; set; }
    public string TaskName { get; set; }
    public int Duration { get; set; }
    public int[] Predecessors { get; set; }
    public bool IsCritical { get; set; }
}