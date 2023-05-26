using System.Text.Json.Serialization;

namespace ProjectPlanner.Business.TransportationProblem;

public class Supplier
{
    public int Supply { get; }
    public float Cost { get; }
    public bool IsFictional { get; set; }
    
    [JsonConstructor]
    public Supplier(int supply, float cost)
    {
        Supply = supply;
        Cost = cost;
        IsFictional = false;
    }

    public Supplier(int supply, int cost, bool isFictional)
    {
        Supply = supply;
        Cost = cost;
        IsFictional = isFictional;
    }
}