using ProjectPlanner.Business.CriticalPathMethod;

namespace ProjectPlanner.Algorithm.Tests;

public class CpmAlgorithmTests
{
    [Fact]
    public void CreateSolution_ShouldReturnExpectedSolution()
    {
        // Arrange
        var cpmTask = new CpmTask
        {
            Activities = new List<CpmActivity>
            {
                new CpmActivity
                {
                    TaskName = "Task 1",
                    Duration = 3,
                    Sequence = new int[] { 0, 1 }
                },
                new CpmActivity
                {
                    TaskName = "Task 2",
                    Duration = 5,
                    Sequence = new int[] { 1, 2 }
                },
                new CpmActivity
                {
                    TaskName = "Task 3",
                    Duration = 2,
                    Sequence = new int[] { 2, 3 }
                },
                new CpmActivity
                {
                    TaskName = "Task 3",
                    Duration = 1,
                    Sequence = new int[] { 1, 3 }
                }
            }
        };
        
        var project = new CpmProject(cpmTask);

        // Act
        var solution = project.CreateSolution();

        // Assert
        Assert.NotNull(solution);
        Assert.NotNull(solution.Activities);
        Assert.NotNull(solution.Events);
        Assert.Equal(solution.Activities.Count, 4);
        Assert.Equal(solution.Events.Count, 4);
        Assert.Equal(solution.Activities[0].TaskName, "Task 1");
        Assert.Equal(solution.Activities[1].TaskName, "Task 2");
        Assert.Equal(solution.Activities[2].TaskName, "Task 3");
        Assert.Equal(solution.Activities[3].TaskName, "Task 3");
        Assert.True(solution.Activities[0].Critical);
        Assert.True(solution.Activities[1].Critical);
        Assert.True(solution.Activities[2].Critical);
        Assert.False(solution.Activities[3].Critical);
        Assert.Equal(solution.Activities[0].TimeReserve, 0);
        Assert.Equal(solution.Activities[1].TimeReserve, 0);
        Assert.Equal(solution.Activities[2].TimeReserve, 0);
        Assert.Equal(solution.Activities[3].TimeReserve, 6);
        Assert.Equal(solution.Activities[0].EarlyStart, 0);
        Assert.Equal(solution.Activities[0].EarlyFinish, 3);
        Assert.Equal(solution.Activities[1].EarlyStart, 3);
        Assert.Equal(solution.Activities[1].EarlyFinish, 8);
        Assert.Equal(solution.Activities[2].EarlyStart, 8);
        Assert.Equal(solution.Activities[2].EarlyFinish, 10);
        Assert.Equal(solution.Activities[3].EarlyStart, 3);
        Assert.Equal(solution.Activities[3].EarlyFinish, 4);
        Assert.Equal(solution.Activities[0].LateStart, 0);
        Assert.Equal(solution.Activities[0].LateFinish, 3);
        Assert.Equal(solution.Activities[1].LateStart, 3);
        Assert.Equal(solution.Activities[1].LateFinish, 8);
        Assert.Equal(solution.Activities[2].LateStart, 8);
        Assert.Equal(solution.Activities[2].LateFinish, 10);
        Assert.Equal(solution.Activities[3].LateStart, 9);
        Assert.Equal(solution.Activities[3].LateFinish, 10);
        Assert.Equal(0, solution.Events[0].Predecessors.Count);
        Assert.Equal(1, solution.Events[0].Successors.Count);
        Assert.Equal(1, solution.Events[1].Predecessors.Count);
        Assert.Equal(2, solution.Events[1].Successors.Count);
        Assert.Equal(1, solution.Events[2].Predecessors.Count);
        Assert.Equal(1, solution.Events[2].Successors.Count);
        Assert.Equal(2, solution.Events[3].Predecessors.Count);
        Assert.Equal(0, solution.Events[3].Successors.Count);
    }
    
    [Fact]
    public void CreateSolution_ShouldIdentifyCriticalActivities()
    {
        // Arrange
        var cpmTask = new CpmTask
        {
            Activities = new List<CpmActivity>
            {
                new CpmActivity
                {
                    TaskName = "Task 1",
                    Duration = 3,
                    Sequence = new int[] { 0, 1 }
                },
                new CpmActivity
                {
                    TaskName = "Task 2",
                    Duration = 5,
                    Sequence = new int[] { 1, 2 }
                },
                new CpmActivity
                {
                    TaskName = "Task 3",
                    Duration = 2,
                    Sequence = new int[] { 2, 3 }
                },
                new CpmActivity
                {
                    TaskName = "Task 3",
                    Duration = 1,
                    Sequence = new int[] { 1, 3 }
                }
            }
        };
        
        var project = new CpmProject(cpmTask);

        // Act
        var solution = project.CreateSolution();

        // Assert

        Assert.True(solution.Activities[0].Critical);
        Assert.True(solution.Activities[1].Critical);
        Assert.True(solution.Activities[2].Critical);
        Assert.False(solution.Activities[3].Critical);
    }
    
    [Fact]
    public void CreateSolution_ShouldIdentifyTimeReserve()
    {
        // Arrange
        var cpmTask = new CpmTask
        {
            Activities = new List<CpmActivity>
            {
                new CpmActivity
                {
                    TaskName = "Task 1",
                    Duration = 3,
                    Sequence = new int[] { 0, 1 }
                },
                new CpmActivity
                {
                    TaskName = "Task 2",
                    Duration = 5,
                    Sequence = new int[] { 1, 2 }
                },
                new CpmActivity
                {
                    TaskName = "Task 3",
                    Duration = 2,
                    Sequence = new int[] { 2, 3 }
                },
                new CpmActivity
                {
                    TaskName = "Task 3",
                    Duration = 1,
                    Sequence = new int[] { 1, 3 }
                }
            }
        };
        
        var project = new CpmProject(cpmTask);

        // Act
        var solution = project.CreateSolution();

        // Assert
        Assert.NotNull(solution);
        Assert.NotNull(solution.Activities);
        Assert.NotNull(solution.Events);
        Assert.Equal(solution.Activities.Count, 4);
        Assert.Equal(solution.Events.Count, 4);

        Assert.Equal(solution.Activities[0].TimeReserve, 0);
        Assert.Equal(solution.Activities[1].TimeReserve, 0);
        Assert.Equal(solution.Activities[2].TimeReserve, 0);
        Assert.Equal(solution.Activities[3].TimeReserve, 6);
    }
    
    [Fact]
    public void CreateSolution_ShouldIdentifyEarlyStart()
    {
        // Arrange
        var cpmTask = new CpmTask
        {
            Activities = new List<CpmActivity>
            {
                new CpmActivity
                {
                    TaskName = "Task 1",
                    Duration = 3,
                    Sequence = new int[] { 0, 1 }
                },
                new CpmActivity
                {
                    TaskName = "Task 2",
                    Duration = 5,
                    Sequence = new int[] { 1, 2 }
                },
                new CpmActivity
                {
                    TaskName = "Task 3",
                    Duration = 2,
                    Sequence = new int[] { 2, 3 }
                },
                new CpmActivity
                {
                    TaskName = "Task 3",
                    Duration = 1,
                    Sequence = new int[] { 1, 3 }
                }
            }
        };
        
        var project = new CpmProject(cpmTask);

        // Act
        var solution = project.CreateSolution();

        // Assert
        Assert.NotNull(solution);
        Assert.NotNull(solution.Activities);
        Assert.NotNull(solution.Events);
        Assert.Equal(solution.Activities.Count, 4);
        Assert.Equal(solution.Events.Count, 4);
        
        Assert.Equal(solution.Activities[0].EarlyStart, 0);
        Assert.Equal(solution.Activities[1].EarlyStart, 3);
        Assert.Equal(solution.Activities[2].EarlyStart, 8);
        Assert.Equal(solution.Activities[3].EarlyStart, 3);
    }
    
    [Fact]
    public void CreateSolution_ShouldIdentifyEarlyFinish()
    {
        // Arrange
        var cpmTask = new CpmTask
        {
            Activities = new List<CpmActivity>
            {
                new CpmActivity
                {
                    TaskName = "Task 1",
                    Duration = 3,
                    Sequence = new int[] { 0, 1 }
                },
                new CpmActivity
                {
                    TaskName = "Task 2",
                    Duration = 5,
                    Sequence = new int[] { 1, 2 }
                },
                new CpmActivity
                {
                    TaskName = "Task 3",
                    Duration = 2,
                    Sequence = new int[] { 2, 3 }
                },
                new CpmActivity
                {
                    TaskName = "Task 3",
                    Duration = 1,
                    Sequence = new int[] { 1, 3 }
                }
            }
        };
        
        var project = new CpmProject(cpmTask);

        // Act
        var solution = project.CreateSolution();

        // Assert
        Assert.NotNull(solution);
        Assert.NotNull(solution.Activities);
        Assert.NotNull(solution.Events);
        Assert.Equal(solution.Activities.Count, 4);

        Assert.Equal(solution.Activities[0].EarlyFinish, 3);
        Assert.Equal(solution.Activities[1].EarlyFinish, 8);
        Assert.Equal(solution.Activities[2].EarlyFinish, 10);
        Assert.Equal(solution.Activities[3].EarlyFinish, 4);
    }
    
    [Fact]
    public void CreateSolution_ShouldIdentifyLateStart()
    {
        // Arrange
        var cpmTask = new CpmTask
        {
            Activities = new List<CpmActivity>
            {
                new CpmActivity
                {
                    TaskName = "Task 1",
                    Duration = 3,
                    Sequence = new int[] { 0, 1 }
                },
                new CpmActivity
                {
                    TaskName = "Task 2",
                    Duration = 5,
                    Sequence = new int[] { 1, 2 }
                },
                new CpmActivity
                {
                    TaskName = "Task 3",
                    Duration = 2,
                    Sequence = new int[] { 2, 3 }
                },
                new CpmActivity
                {
                    TaskName = "Task 3",
                    Duration = 1,
                    Sequence = new int[] { 1, 3 }
                }
            }
        };
        
        var project = new CpmProject(cpmTask);

        // Act
        var solution = project.CreateSolution();

        // Assert
        Assert.NotNull(solution);
        Assert.NotNull(solution.Activities);
        Assert.Equal(solution.Activities.Count, 4);

        Assert.Equal(solution.Activities[0].LateStart, 0);
        Assert.Equal(solution.Activities[1].LateStart, 3);
        Assert.Equal(solution.Activities[2].LateStart, 8);
        Assert.Equal(solution.Activities[3].LateStart, 9);
    }
    
    [Fact]
    public void CreateSolution_ShouldIdentifyLateFinish()
    {
        // Arrange
        var cpmTask = new CpmTask
        {
            Activities = new List<CpmActivity>
            {
                new CpmActivity
                {
                    TaskName = "Task 1",
                    Duration = 3,
                    Sequence = new int[] { 0, 1 }
                },
                new CpmActivity
                {
                    TaskName = "Task 2",
                    Duration = 5,
                    Sequence = new int[] { 1, 2 }
                },
                new CpmActivity
                {
                    TaskName = "Task 3",
                    Duration = 2,
                    Sequence = new int[] { 2, 3 }
                },
                new CpmActivity
                {
                    TaskName = "Task 3",
                    Duration = 1,
                    Sequence = new int[] { 1, 3 }
                }
            }
        };
        
        var project = new CpmProject(cpmTask);

        // Act
        var solution = project.CreateSolution();

        // Assert
        Assert.NotNull(solution);
        Assert.NotNull(solution.Activities);
        Assert.Equal(solution.Activities.Count, 4);
        
        Assert.Equal(solution.Activities[0].LateFinish, 3);
        Assert.Equal(solution.Activities[1].LateFinish, 8);
        Assert.Equal(solution.Activities[2].LateFinish, 10);
        Assert.Equal(solution.Activities[3].LateFinish, 10);
    }
    
    [Fact]
    public void CreateSolution_ShouldIdentifyPredecessors()
    {
        // Arrange
        var cpmTask = new CpmTask
        {
            Activities = new List<CpmActivity>
            {
                new CpmActivity
                {
                    TaskName = "Task 1",
                    Duration = 3,
                    Sequence = new int[] { 0, 1 }
                },
                new CpmActivity
                {
                    TaskName = "Task 2",
                    Duration = 5,
                    Sequence = new int[] { 1, 2 }
                },
                new CpmActivity
                {
                    TaskName = "Task 3",
                    Duration = 2,
                    Sequence = new int[] { 2, 3 }
                },
                new CpmActivity
                {
                    TaskName = "Task 3",
                    Duration = 1,
                    Sequence = new int[] { 1, 3 }
                }
            }
        };
        
        var project = new CpmProject(cpmTask);

        // Act
        var solution = project.CreateSolution();

        // Assert
        Assert.NotNull(solution);
        Assert.NotNull(solution.Activities);
        Assert.Equal(solution.Activities.Count, 4);

        Assert.Equal(0, solution.Events[0].Predecessors.Count);
        Assert.Equal(1, solution.Events[0].Successors.Count);
        Assert.Equal(1, solution.Events[1].Predecessors.Count);
        Assert.Equal(2, solution.Events[1].Successors.Count);
        Assert.Equal(1, solution.Events[2].Predecessors.Count);
        Assert.Equal(1, solution.Events[2].Successors.Count);
        Assert.Equal(2, solution.Events[3].Predecessors.Count);
        Assert.Equal(0, solution.Events[3].Successors.Count);
    }
}