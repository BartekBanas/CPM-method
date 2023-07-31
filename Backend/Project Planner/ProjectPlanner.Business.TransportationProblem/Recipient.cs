using System.Text.Json.Serialization;

namespace ProjectPlanner.Business.TransportationProblem;

public class Recipient
{
    public int Demand { get; set; }
    public float Cost { get; set; }
    public bool IsFictional { get; set; }

    public Recipient()
    {
    }
    
    [JsonConstructor]
    public Recipient(int demand, float cost)
    {
        Demand = demand;
        Cost = cost;
        IsFictional = false;
    }

    public Recipient(int demand, int cost, bool isFictional)
    {
        Demand = demand;
        Cost = cost;
        IsFictional = isFictional;
    }
    
    public Recipient DeepCopy()
    {
        return new Recipient
        {
            Demand = Demand,
            Cost = Cost,
            IsFictional = IsFictional
        };
    }
}