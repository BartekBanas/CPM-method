using Microsoft.AspNetCore.Mvc;
using ProjectPlanner.Application.Services;
using ProjectPlanner.Business.TransportProblem.Dtos;

namespace Project_Planner.Controllers;

[ApiController]
[Route("api/TP")]
public class TpController : Controller
{
    private readonly TpService _tpService;

    public TpController(TpService tpService)
    {
        _tpService = tpService;
    }
    
    public async Task<IActionResult> PostCpmRequest([FromBody] TpTask task)
    {
        // var validationResult = await _validator.ValidateAsync(task);
        //
        // if (!validationResult.IsValid)
        // {
        //     return BadRequest(validationResult.Errors);
        // }
        
        var solution = await _tpService.Solve(task);

        return Ok(solution);
    }
}