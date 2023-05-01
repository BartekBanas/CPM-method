namespace ProjectPlanner.Business.TransportationProblem;

public class Supplier
{
    public int Supply { get; }
    public float Cost { get; }
    
    public Supplier(int supply, float cost)
    {
        Supply = supply;
        Cost = cost;
    }
}