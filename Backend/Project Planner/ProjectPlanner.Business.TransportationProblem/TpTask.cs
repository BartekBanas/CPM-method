namespace ProjectPlanner.Business.TransportationProblem;

public class TpTask
{
    public Supplier[] Suppliers { get; set; }
    public Recipient[] Recipients { get; set; }

    public float[][] TransportCost { get; set; }

    public TpTask(Supplier[] suppliers, Recipient[] recipients, float[][] transportCost)
    {
        Suppliers = suppliers;
        Recipients = recipients;
        TransportCost = transportCost;
    }
}