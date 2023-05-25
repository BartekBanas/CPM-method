namespace ProjectPlanner.Business.TransportationProblem;

public class TpProject
{
    private TpTask _task;

    public float[,] TransportationTable { get; set; }
    
    private float[,] _profitTable;
    
    public TpProject(TpTask task)
    {
        _task = task;
        InitializeTpProject();
    }

    public TpSolution CreateSolution()
    {
        throw new NotImplementedException();
    }

    private void InitializeTpProject()
    {
        int totalSupply = _task.Suppliers.Sum(supplier => supplier.Supply);
        int totalDemand = _task.Recipients.Sum(recipient => recipient.Demand);

        if (totalSupply > totalDemand)
        {
            _task.Recipients.Add(new Recipient(totalSupply - totalDemand, 0));
        }
        if (totalSupply < totalDemand)
        {
            _task.Suppliers.Add(new Supplier(totalSupply - totalDemand, 0));
        }

        InitializeProfitTable();
    }

    private void InitializeProfitTable()
    {
        _profitTable = new float[_task.Suppliers.Count, _task.Recipients.Count];

        int transportationCostsArrayLenght = _task.TransportCost.Length;
        int transportationCostsArrayWidth = _task.TransportCost[0].Length;
        
        for (int i = 0; i < _task.Suppliers.Count; i++)
        {
            for (int j = 0; j < _task.Recipients.Count; j++)
            {
                //Profit equals to selling cost to recipients - buying cost from suppliers
                _profitTable[i, j] = _task.Recipients[j].Cost - _task.Suppliers[i].Cost;
            }
        }

        for (int i = 0; i < transportationCostsArrayLenght; i++)
        {
            for (int j = 0; j < transportationCostsArrayWidth; j++)
            {
                _profitTable[i, j] -= _task.TransportCost[i][j];
            }
        }
    }
}