using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProjectPlanner.Application.Services;
using ProjectPlanner.Business.CriticalPathMethod.Entities;

namespace Project_Planner.Controllers;

[ApiController]
[Route("api/CPM")]
public class CpmController : Controller
{
    private readonly ICpmService _cpmService;
    private readonly IValidator<CpmTask> _validator;
    
    public CpmController(ICpmService cpmService, IValidator<CpmTask> validator)
    {
        _cpmService = cpmService;
        _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> PostCpmRequest([FromBody] CpmTask task)
    {
        var validationResult = await _validator.ValidateAsync(task);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        
        var solution = await _cpmService.Solve(task);

        return Ok(solution);
    }
}