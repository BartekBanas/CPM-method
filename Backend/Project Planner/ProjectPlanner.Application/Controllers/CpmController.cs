using Microsoft.AspNetCore.Mvc;
using ProjectPlanner.Application.Services;
using ProjectPlanner.Business.Validation;
using ProjectPlanner.Infrastructure.TaskObjects;

namespace Project_Planner.Controllers;

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
        var validator = new CpmTaskValidator(task);
        if (validator.Validate() == false)
        {
            return BadRequest(validator.ErrorMessage);
        }
        
        //var solution = await _cpmService.Solve(task);

        return Ok();
    }
}