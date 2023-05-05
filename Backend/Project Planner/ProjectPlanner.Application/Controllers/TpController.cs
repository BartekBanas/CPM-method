using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProjectPlanner.Application.Services;
using ProjectPlanner.Business.TransportationProblem;

namespace Project_Planner.Controllers;

[ApiController]
[Route("api/TP")]
public class TpController : Controller
{
    private readonly ITpService _tpService;
    private readonly IValidator<TpTask> _validator;

    public TpController(ITpService tpService, IValidator<TpTask> validator)
    {
        _tpService = tpService;
        _validator = validator;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostTpRequest([FromBody] TpTask task)
    {
        var validationResult = await _validator.ValidateAsync(task);
        
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        
        var solution = await _tpService.Solve(task);

        return Ok(solution);
    }
}