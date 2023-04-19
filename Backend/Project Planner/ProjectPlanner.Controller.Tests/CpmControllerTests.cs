using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Project_Planner.Controllers;
using ProjectPlanner.Application.Services;
using ProjectPlanner.Business.CriticalPathMethod.Dtos;
using Xunit.Abstractions;

namespace ProjectPlanner.Controller.Tests;

public class CpmControllerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly Mock<ICpmService> _cpmServiceMock;
    private readonly Mock<IValidator<CpmTask>> _validatorMock;
    private readonly CpmController _controller;

    public CpmControllerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _cpmServiceMock = new Mock<ICpmService>();
        _validatorMock = new Mock<IValidator<CpmTask>>();
        _controller = new CpmController(_cpmServiceMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task PostCpmRequest_ReturnsOkResult_ForValidRequest()
    {
        // Arrange
        var json = @" 
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

        var task = JsonConvert.DeserializeObject<CpmTask>(json);

        _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<CpmTask>(), CancellationToken.None))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _cpmServiceMock.Setup(c => c.Solve(task)).ReturnsAsync(new CpmSolution());

        // Act
        var result = await _controller.PostCpmRequest(task);

        _testOutputHelper.WriteLine(result.ToString());

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task PostCpmRequest_ReturnsBadRequest_ForInvalidRequestWithCycle()
    {
        // Arrange
        var json = @"{
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
                ""taskName"": ""Task 3"",
                ""duration"": 2,
                ""sequence"": [3, 4]
            },
            {
                ""taskName"": ""Task 3"",
                ""duration"": 2,
                ""sequence"": [4, 2]
            },
            {
                ""taskName"": ""Task 3"",
                ""duration"": 2,
                ""sequence"": [4, 5]
            }
        ]
    }";

        var task = JsonConvert.DeserializeObject<CpmTask>(json);

        _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<CpmTask>(), CancellationToken.None))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        // Act
        var result = await _controller.PostCpmRequest(task);

        _testOutputHelper.WriteLine(result.ToString());

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var errors = Assert.IsType<List<FluentValidation.Results.ValidationFailure>>(badRequestResult.Value);
        Assert.NotEmpty(errors);
        Assert.Equal("A cyclic dependency between activities has been detected", errors[0].ErrorMessage);
    }
    
    [Fact]
    public async Task PostCpmRequest_ReturnsBadRequest_ForInvalidRequestWithInvalidSequence()
    {
        // Arrange
        var json = @"{
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

        var task = JsonConvert.DeserializeObject<CpmTask>(json);

        _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<CpmTask>(), CancellationToken.None))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult
            {
                Errors =
                {
                    new FluentValidation.Results.ValidationFailure("Activities", "Activity 2 cannot come in between one and the same event"),
                    new FluentValidation.Results.ValidationFailure("Activities", "More than one starting event found"),
                    new FluentValidation.Results.ValidationFailure("", "A cyclic dependency between activities has been detected")
                }
            });

        // Act
        var result = await _controller.PostCpmRequest(task);

        _testOutputHelper.WriteLine(result.ToString());

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var errors = Assert.IsType<List<FluentValidation.Results.ValidationFailure>>(badRequestResult.Value);
        Assert.Equal(3, errors.Count);
        Assert.Equal("Activity 2 cannot come in between one and the same event", errors[0].ErrorMessage);
        Assert.Equal("More than one starting event found", errors[1].ErrorMessage);
        Assert.Equal("A cyclic dependency between activities has been detected", errors[2].ErrorMessage);
    }
}