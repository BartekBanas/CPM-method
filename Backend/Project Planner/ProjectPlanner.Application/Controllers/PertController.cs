using Microsoft.AspNetCore.Mvc;
using ProjectPlanner.Application.Services;
using ProjectPlanner.Infrastructure.TaskObjects;

namespace Project_Planner.Controllers;

[ApiController]
[Microsoft.AspNetCore.Components.Route("api/PERT")]
public class PertController : Controller
{
    private readonly PertService _pertService;

    public PertController(PertService pertService)
    {
        _pertService = pertService;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostCpmRequest([FromBody] PertTask task)
    {
        var solution = await _pertService.Solve(task);

        return Ok(solution);
    }
}