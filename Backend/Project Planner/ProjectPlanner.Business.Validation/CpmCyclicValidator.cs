using ProjectPlanner.Infrastructure.TaskObjects;

namespace ProjectPlanner.Business.Validation;

public class CpmCyclicValidator
{
    public CpmTask Task { get; }
    
    public CpmCyclicValidator(CpmTask task)
    {
        Task = task;
    }
    
    public bool Validate()
    {
        Stack<int> stack = new Stack<int>();
        bool[] visited = new bool[Task.Activities.Count];
        bool[] stackFlag = new bool[Task.Activities.Count];

        // DFS
        for (int i = 0; i < Task.Activities.Count; i++)
        {
            if (!visited[i])
            {
                if (HasCycle(i, visited, stack, stackFlag))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool HasCycle(int v, bool[] visited, Stack<int> stack, bool[] stackFlag)
    {
        visited[v] = true;
        stack.Push(v);
        stackFlag[v] = true;

        foreach (var neighborIndex in Task.Activities[v].Sequence)
        {
            if (!visited[neighborIndex])
            {
                if (HasCycle(neighborIndex, visited, stack, stackFlag))
                {
                    return true;
                }
            }
            else if (stackFlag[neighborIndex])
            {
                return true;
            }
        }

        stackFlag[v] = false;
        stack.Pop();
        return false;
    }

}