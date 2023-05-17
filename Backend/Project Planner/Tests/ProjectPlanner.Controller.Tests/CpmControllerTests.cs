using ProjectPlanner.Business.CriticalPathMethod;

namespace ProjectPlanner.Controller.Tests;

public class CpmControllerTests
{
    private readonly CpmController _controller;

    public CpmControllerTests()
    {
        ICpmService cpmService = new CpmService(new CpmFluentValidator());
        _controller = new CpmController(cpmService);
    }

    [Fact]
    public async Task PostCpmRequest_ReturnsOkResult_ForValidRequest()
    {
        // Arrange
        const string jsonCpmTask = @" 
        {
            ""activities"": [
                {
                    ""taskName"": ""Task 1"",
                    ""duration"": 3,
                    ""sequence"": [0, 1]
                },
                {
                    ""taskName"": ""Task 2"",
                    ""duration"": 5,
                    ""sequence"": [1, 2]
                },
                {
                    ""taskName"": ""Task 3"",
                    ""duration"": 2,
                    ""sequence"": [2, 3]
                }
            ]
        }";

        var task = JsonConvert.DeserializeObject<CpmTask>(jsonCpmTask);

        // Act
        if (task != null)
        {
            var result = await _controller.PostCpmRequest(task);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        
        // Assert
        Assert.NotNull(task);
    }

    [Fact]
    public async Task PostCpmRequest_ReturnsBadRequest_ForInvalidRequestWithCycle()
    {
        // Arrange
        const string jsonCpmTask = @"{
        ""activities"": [
            {
                ""taskName"": ""Task 1"",
                ""duration"": 3,
                ""sequence"": [0, 1]
            },
            {
                ""taskName"": ""Task 2"",
                ""duration"": 5,
                ""sequence"": [1, 2]
            },
            {
                ""taskName"": ""Task 3"",
                ""duration"": 2,
                ""sequence"": [2, 3]
            },
            {
                ""taskName"": ""Task 4"",
                ""duration"": 2,
                ""sequence"": [3, 4]
            },
            {
                ""taskName"": ""Task 5"",
                ""duration"": 2,
                ""sequence"": [4, 2]
            },
            {
                ""taskName"": ""Task 6"",
                ""duration"": 2,
                ""sequence"": [4, 5]
            }
        ]
    }";

        var task = JsonConvert.DeserializeObject<CpmTask>(jsonCpmTask);

        // Act
        if (task != null)
        {
            var result = await _controller.PostCpmRequest(task);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        
        // Assert
        Assert.NotNull(task);
    }

    [Fact]
    public async Task PostCpmRequest_ReturnsBadRequest_ForInvalidRequestWithInvalidSequence()
    {
        // Arrange
        const string jsonCpmTask = @"{
        ""activities"": [
            {
                ""taskName"": ""Task 1"",
                ""duration"": 3,
                ""sequence"": [0, 1]
            },
            {
                ""taskName"": ""Task 2"",
                ""duration"": 5,
                ""sequence"": [1, 1]
            },
            {
                ""taskName"": ""Task 3"",
                ""duration"": 2,
                ""sequence"": [2, 3]
            }
        ]
    }";

        var task = JsonConvert.DeserializeObject<CpmTask>(jsonCpmTask);

        // Act
        if (task != null)
        {
            var result = await _controller.PostCpmRequest(task);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errors = Assert.IsType<List<FluentValidation.Results.ValidationFailure>>(badRequestResult.Value);
            Assert.Equal(3, errors.Count);
            Assert.Equal("Activity 2 cannot come in between one and the same event", errors[0].ErrorMessage);
            Assert.Equal("More than one starting event found", errors[1].ErrorMessage);
            Assert.Equal("A cyclic dependency between activities has been detected", errors[2].ErrorMessage);
        }
        
        // Assert
        Assert.NotNull(task);
    }
}