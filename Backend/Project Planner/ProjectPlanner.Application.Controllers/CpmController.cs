using Microsoft.AspNetCore.Mvc;
using ProjectPlanner.Application.Services;
using ProjectPlanner.Business.CriticalPathMethod;

namespace ProjectPlanner.Application.Controllers;

[ApiController]
[Route("api/CPM")]
public class CpmController : Controller
{
    private readonly ICpmService _cpmService;
    public CpmController(ICpmService cpmService)
    {
        _cpmService = cpmService;
    }

    [HttpPost]
    public async Task<IActionResult> PostCpmRequest([FromBody] CpmTask task)
    {
        var validationResult = await _cpmService.Validate(task);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        
        var solution = await _cpmService.Solve(task);

        return Ok(solution);
    }
}