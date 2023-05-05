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
            ""Suppliers"": [
                {
                    ""Supply"": 100,
                    ""Cost"": 5.5
                },
                {
                    ""Supply"": 200,
                    ""Cost"": 4.5
                },
                {
                    ""Supply"": 150,
                    ""Cost"": 6.0
                }
            ],
            ""Recipients"": [
                {
                    ""Demand"": 80,
                    ""Cost"": 5.0
                },
                {
                    ""Demand"": 150,
                    ""Cost"": 6.0
                },
                {
                    ""Demand"": 120,
                    ""Cost"": 4.5
                }
            ],
            ""TransportCost"": [
                [2.5, 3.0, 2.0],
                [3.5, 2.0, 4.0],
                [1.5, 2.5, 3.0]
        ]}";

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
    
    [Fact]
    public async Task PostTpRequest_ReturnsBadRequest_ForInvalidRequestWithInvalidCosts()
    {
        // Arrange
        const string jsonTpTask = @" 
        {
            ""Suppliers"": [
                {
                    ""Supply"": 100,
                    ""Cost"": 5.5
                },
                {
                    ""Supply"": 200,
                    ""Cost"": 4.5
                },
                {
                    ""Supply"": 150,
                    ""Cost"": 6.0
                }
            ],
            ""Recipients"": [
                {
                    ""Demand"": 80,
                    ""Cost"": 5.0
                },
                {
                    ""Demand"": 150,
                    ""Cost"": 6.0
                },
                {
                    ""Demand"": 120,
                    ""Cost"": 4.5
                }
            ],
            ""TransportCost"": [
                [2.5, 3.0, 2.0],
                [-3.5, 2.0, 4.0],
                [1.5, 2.5, 3.0]
        ]}";

        var task = JsonConvert.DeserializeObject<TpTask>(jsonTpTask);

        // Act
        if (task != null)
        {
            var result = await _controller.PostTpRequest(task);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errors = Assert.IsType<List<FluentValidation.Results.ValidationFailure>>(badRequestResult.Value);
            Assert.Equal(1, errors.Count);
            Assert.Equal("Transportation cost between Supplier 2 and recipient 1 cannot be negative", errors[0].ErrorMessage);
        }
        
        // Assert
        Assert.NotNull(task);
    }
}