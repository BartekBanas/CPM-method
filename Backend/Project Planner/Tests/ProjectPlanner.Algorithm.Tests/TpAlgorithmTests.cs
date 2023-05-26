using ProjectPlanner.Business.TransportationProblem;

namespace ProjectPlanner.Algorithm.Tests;

public class TpAlgorithmTests
{
    [Fact]
    public void CreateSolution_ShouldIdentifyTransportationTable()
    {
        // Arrange
        List<Supplier> suppliers = new List<Supplier>
        {
            new Supplier(20, 10),
            new Supplier(30, 12)
        };

        List<Recipient> recipients = new List<Recipient>
        {
            new Recipient(10, 30),
            new Recipient(28, 25),
            new Recipient(27, 30)
        };

        float[][] transportCost = new float[][]
        {
            new float[] { 8, 14, 17 },
            new float[] { 12, 9, 19 }
        };

        TpTask tpTask = new TpTask(suppliers, recipients, transportCost);

        var project = new TpProject(tpTask);
        
        // Act
        var solution = project.CreateSolution();
        
        // Assert
        Assert.NotNull(solution);
        Assert.NotNull(solution.TransportationTable);
        Assert.Equal(2, solution.TransportationTable.Length);
        Assert.Equal(3, solution.TransportationTable[0].Length);
    }
}