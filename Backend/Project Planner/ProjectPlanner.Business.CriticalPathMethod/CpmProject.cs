using ProjectPlanner.Business.CriticalPathMethod.Dtos;

namespace ProjectPlanner.Business.CriticalPathMethod;

public class CpmProject
{
    public CpmSolution CreateSolution(CpmTask task)
    {
        return new CpmSolution();
    }
}