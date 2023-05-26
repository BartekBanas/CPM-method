namespace ProjectPlanner.Business.TransportationProblem;

public class TpTask
{
    public List<Supplier> Suppliers { get; set; }
    public List<Recipient> Recipients { get; set; }

    public float[][] TransportCost { get; set; }

    public TpTask(List<Supplier> suppliers, List<Recipient> recipients, float[][] transportCost)
    {
        Suppliers = suppliers;
        Recipients = recipients;
        TransportCost = transportCost;
    }
}