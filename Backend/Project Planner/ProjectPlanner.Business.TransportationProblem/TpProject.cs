namespace ProjectPlanner.Business.TransportationProblem;

public class TpProject
{
    private TpTask _task;

    public float[][] TransportationTable { get; set; }
    
    public TpProject(TpTask task)
    {
        _task = task;
        InitializeTpProject();
    }

    public TpSolution CreateSolution()
    {
        throw new NotImplementedException();
    }

    private void InitializeTpProject()
    {
        
    }
}