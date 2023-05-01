namespace ProjectPlanner.Business.TransportationProblem;

public class Supplier
{
    public int Supply { get; set; }
    public float Cost { get; set; }
    
    public Supplier(int supply, float cost)
    {
        Supply = supply;
        Cost = cost;
    }
}