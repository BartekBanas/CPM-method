using ProjectPlanner.Business.TransportationProblem.Dtos;

namespace ProjectPlanner.Controller.Tests;

public class TpControllerTests
{
    private readonly TpController _controller;

    public TpControllerTests()
    {
        ITpService tpService = new TpService();
        IValidator<TpTask> validator = new TpFluentValidator();
        _controller = new TpController(tpService, validator);
    }

    [Fact]
    public async Task PostTpRequest_ReturnsOkResult_ForValidRequest()
    {
        // Arrange
        const string jsonTpTask = @" 
        {
            {
                ""Suppliers"": [
            {
                ""Supply"": 10,
                ""Cost"": 5.0
            },
            {
                ""Supply"": 20,
                ""Cost"": 10.0
            }
            ],
            ""Recipients"": [
            {
                ""Demand"": 5,
                ""Cost"": 8.0
            },
            {
                ""Demand"": 25,
                ""Cost"": 12.0
            },
            {
                ""Demand"": 15,
                ""Cost"": 9.0
            }
            ],
            ""TransportCost"": [
            [5.0, 7.0, 6.0],
            [4.0, 2.0, 8.0]
            ]
        }}";

        var task = JsonConvert.DeserializeObject<TpTask>(jsonTpTask);

        // Act
        if (task != null)
        {
            var result = await _controller.PostTpRequest(task);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}