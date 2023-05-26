namespace ProjectPlanner.Business.TransportationProblem;

public class TpProject
{
    private TpTask _task;
    private float[,] TransportationTable { get; set; } = null!;

    private float[,] _profitTable = null!;
    
    public TpProject(TpTask task)
    {
        _task = task;
        InitializeTpProject();
    }

    public TpSolution CreateSolution()
    {
        //Tables size is reduced to the original one to exclude fictional actors
        var jaggedTransportationTable = ConvertToJaggedArray(TransportationTable, _task.TransportCost.Length,
            _task.TransportCost[0].Length);
        
        return new TpSolution(0, 0, 0, jaggedTransportationTable);
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
        InitializeTransportationTable();
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

    private void InitializeTransportationTable()
    {
        TransportationTable = new float[_task.Suppliers.Count, _task.Recipients.Count];

        int[] supplyToDistribute = new int[_task.Suppliers.Count];
        int[] demandToDistribute = new int[_task.Recipients.Count];

        for (int i = 0; i < _task.Suppliers.Count; i++)
        {
            supplyToDistribute[i] = _task.Suppliers[i].Supply;
        }
        
        for (int i = 0; i < _task.Recipients.Count; i++)
        {
            demandToDistribute[i] = _task.Recipients[i].Demand;
        }
        
        IEnumerable<(int, int)> sortedIndicesArray = CreateSortedIndicesArray(_profitTable);

        foreach (var indexes in sortedIndicesArray)
        {
            int supplierId = indexes.Item1;
            int recipientId = indexes.Item2;
            
            int amountOfCargo = Math.Min(supplyToDistribute[supplierId], demandToDistribute[recipientId]);
            
            TransportationTable[supplierId, recipientId] = amountOfCargo;
            
            supplyToDistribute[supplierId] -= amountOfCargo;
            demandToDistribute[recipientId] -= amountOfCargo;
        }
    }
    
    private static IEnumerable<(int, int)> CreateSortedIndicesArray(float[,] array)
    {
        (int, int)[] indexPairs = new (int, int)[array.Length];
        int index = 0;
        
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                indexPairs[index] = (i, j);
                index++;
            }
        }
        
        var sortedIndexPairs = indexPairs.OrderByDescending(pair => array[pair.Item1, pair.Item2]);
        
        return sortedIndexPairs;
    }

    private static float[][] ConvertToJaggedArray(float[,] array, int rows, int columns)
    {
        var jaggedArray = new float[rows][];

        for (int i = 0; i < rows; i++)
        {
            jaggedArray[i] = new float[columns];

            for (int j = 0; j < columns; j++)
            {
                jaggedArray[i][j] = array[i, j];
            }
        }

        return jaggedArray;
    }
}