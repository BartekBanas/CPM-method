using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Project_Planner.Controllers;
using ProjectPlanner.Application.Services;
using ProjectPlanner.Business.CriticalPathMethod;
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
        CpmTask task = new CpmTask
        {
            Activities = new List<CpmActivity>
            {
                new CpmActivity("Task 1", 3, new int[] { 0, 1 }),
                new CpmActivity("Task 2", 5, new int[] { 1, 2 }),
                new CpmActivity("Task 3", 2, new int[] { 2, 3 })
            }
        };

        //_validatorMock.Setup(v => v.ValidateAsync(task)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _cpmServiceMock.Setup(c => c.Solve(task)).ReturnsAsync(new CpmSolution());

        // Act
        var result = await _controller.PostCpmRequest(task);

        _testOutputHelper.WriteLine(result.ToString());

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
}