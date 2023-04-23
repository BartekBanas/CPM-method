using ProjectPlanner.Business.CriticalPathMethod;
using ProjectPlanner.Business.CriticalPathMethod.Dtos;

namespace ProjectPlanner.Business.Validation;

public class CpmCyclicValidator
{
    private readonly List<CpmActivity> _activities;
    private readonly Dictionary<int, bool> _visited;
    private readonly Dictionary<int, bool> _recStack;

    public CpmCyclicValidator(CpmTask task)
    {
        _activities = task.Activities;
        _visited = new Dictionary<int, bool>();
        _recStack = new Dictionary<int, bool>();

        // Dictionaries initialization
        foreach (var activity in _activities)
        {
            _visited[activity.Sequence[0]] = false;
            _visited[activity.Sequence[1]] = false;
            _recStack[activity.Sequence[0]] = false;
            _recStack[activity.Sequence[1]] = false;
        }
    }

    public bool Validate()
    {
        // Iteration over all vertices of the graph
        foreach (var activity in _activities)
        {
            if (!_visited[activity.Sequence[0]])
            {
                if (IsCyclic(activity.Sequence[0]))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool IsCyclic(int v)
    {
        _visited[v] = true;
        _recStack[v] = true;
        
        foreach (var activity in _activities)
        {
            if (activity.Sequence[0] == v)
            {
                if (!_visited[activity.Sequence[1]])
                {
                    if (IsCyclic(activity.Sequence[1]))
                    {
                        return true;
                    }
                }
                else if (_recStack[activity.Sequence[1]])
                {
                    return true;
                }
            }
        }
        
        _recStack[v] = false;

        return false;
    }
}