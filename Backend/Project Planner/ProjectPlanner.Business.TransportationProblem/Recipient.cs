namespace ProjectPlanner.Business.TransportationProblem;

public class Recipient
{
    public int Demand { get; set; }
    public float Cost { get; set; }

    public Recipient(int demand, float cost)
    {
        Demand = demand;
        Cost = cost;
    }
}