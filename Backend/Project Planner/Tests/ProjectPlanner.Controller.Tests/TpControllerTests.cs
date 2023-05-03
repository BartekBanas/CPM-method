using ProjectPlanner.Business.TransportationProblem.Dtos;

namespace ProjectPlanner.Controller.Tests;

public class TpControllerTests
{
    private readonly TpController _controller;

    public TpControllerTests()
    {
        ITpService cpmService = new TpService();
        IValidator<TpTask> validator = new TpFluentValidator();
        _controller = new TpController(cpmService, validator);
    }
}