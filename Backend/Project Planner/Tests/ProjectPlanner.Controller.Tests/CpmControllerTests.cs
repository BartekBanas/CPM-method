using ProjectPlanner.Business.CriticalPathMethod.Dtos;
using ProjectPlanner.Business.Validation;

namespace ProjectPlanner.Controller.Tests;

public class CpmControllerTests
{
    private readonly ICpmService _cpmServiceMock;
    private readonly IValidator<CpmTask> _validatorMock;
    private readonly CpmController _controller;

    public CpmControllerTests(ITestOutputHelper testOutputHelper)
    {
        _cpmServiceMock = new CpmService();
        _validatorMock = new CpmFluentValidator();
        _controller = new CpmController(_cpmServiceMock, _validatorMock);
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

        // _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<CpmTask>(), CancellationToken.None))
        //     .ReturnsAsync(new FluentValidation.Results.ValidationResult());
        //
        // _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<CpmTask>(), CancellationToken.None))
        //     .ReturnsAsync(new FluentValidation.Results.ValidationResult
        //     {
        //         Errors = { new FluentValidation.Results.ValidationFailure() }
        //     });
        
        //_cpmServiceMock.Setup(c => c.Solve(task)).ReturnsAsync(new CpmSolution());

        // Act
        var result = await _controller.PostCpmRequest(task);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
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

        // _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<CpmTask>(), CancellationToken.None))
        //     .ReturnsAsync(new FluentValidation.Results.ValidationResult
        //     {
        //         Errors =
        //         {
        //             new FluentValidation.Results.ValidationFailure("",
        //                 "A cyclic dependency between activities has been detected")
        //         }
        //     });

        // Act
        var result = await _controller.PostCpmRequest(task);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        //Assert.IsType<BadRequestObjectResult>(result);
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

        // _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<CpmTask>(), CancellationToken.None))
        //     .ReturnsAsync(new FluentValidation.Results.ValidationResult
        //     {
        //         Errors =
        //         {
        //             new FluentValidation.Results.ValidationFailure("Activities",
        //                 "Activity 2 cannot come in between one and the same event"),
        //             new FluentValidation.Results.ValidationFailure("Activities", "More than one starting event found"),
        //             new FluentValidation.Results.ValidationFailure("",
        //                 "A cyclic dependency between activities has been detected")
        //         }
        //     });

        // Act
        var result = await _controller.PostCpmRequest(task);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var errors = Assert.IsType<List<FluentValidation.Results.ValidationFailure>>(badRequestResult.Value);
        Assert.Equal(3, errors.Count);
        Assert.Equal("Activity 2 cannot come in between one and the same event", errors[0].ErrorMessage);
        Assert.Equal("More than one starting event found", errors[1].ErrorMessage);
        Assert.Equal("A cyclic dependency between activities has been detected", errors[2].ErrorMessage);
    }
}