namespace ProjectPlanner.Business.TransportationProblem;

public class TpTaskBuilder
{
    private List<Supplier> _suppliers;
    private List<Recipient> _recipients;
    private float[][] _transportCost;

    public TpTaskBuilder()
    {
        _suppliers = new List<Supplier>();
        _recipients = new List<Recipient>();
        _transportCost = null!;
    }

    public TpTaskBuilder WithSuppliers(List<Supplier> suppliers)
    {
        _suppliers = suppliers;
        return this;
    }

    public TpTaskBuilder WithRecipients(List<Recipient> recipients)
    {
        _recipients = recipients;
        return this;
    }

    public TpTaskBuilder WithTransportCost(float[][] transportCost)
    {
        _transportCost = transportCost;
        return this;
    }

    public TpTask Build()
    {
        if (_suppliers == null)
        {
            throw new InvalidOperationException("Suppliers must be set.");
        }

        if (_recipients == null)
        {
            throw new InvalidOperationException("Recipients must be set.");
        }

        if (_transportCost == null)
        {
            throw new InvalidOperationException("Transport cost must be set.");
        }

        return new TpTask(_suppliers, _recipients, _transportCost);
    }
}