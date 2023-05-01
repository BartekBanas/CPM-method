namespace ProjectPlanner.Business.TransportationProblem;

public class Recipient
{
    public int Demand { get; }
    public float Cost { get; }

    public Recipient(int demand, float cost)
    {
        Demand = demand;
        Cost = cost;
    }
}