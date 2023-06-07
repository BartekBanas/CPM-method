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
    
    public TpTask DeepCopy()
    {
        List<Supplier> copiedSuppliers = Suppliers.Select(supplier => supplier.DeepCopy()).ToList();

        List<Recipient> copiedRecipients = Recipients.Select(recipient => recipient.DeepCopy()).ToList();

        var copiedTransportCost = new float[TransportCost.Length][];
        for (int i = 0; i < TransportCost.Length; i++)
        {
            copiedTransportCost[i] = new float[TransportCost[i].Length];
            Array.Copy(TransportCost[i], copiedTransportCost[i], TransportCost[i].Length);
        }

        return new TpTask(copiedSuppliers, copiedRecipients, copiedTransportCost);
    }
}