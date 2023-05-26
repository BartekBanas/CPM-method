using System.Text.Json.Serialization;

namespace ProjectPlanner.Business.TransportationProblem;

public class Recipient
{
    public int Demand { get; }
    public float Cost { get; }
    public bool IsFictional { get; set; }

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
}