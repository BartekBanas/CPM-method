namespace ProjectPlanner.Business.TransportationProblem;

public class TpSolution
{
    public float TotalCost { get; set; }
    public float TotalRevenue { get; set; }
    public float TotalProfit { get; set; }

    public float[][] OptimalTransportationTable { get; set; }

    public TpSolution(float totalCost, float totalRevenue, float totalProfit, float[][] optimalTransportationTable)
    {
        TotalCost = totalCost;
        TotalRevenue = totalRevenue;
        TotalProfit = totalProfit;
        OptimalTransportationTable = optimalTransportationTable ??
                                     throw new ArgumentNullException(nameof(optimalTransportationTable));
    }

    public TpSolution()
    {
    }
}