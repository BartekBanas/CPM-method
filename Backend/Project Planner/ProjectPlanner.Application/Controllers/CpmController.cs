using Microsoft.AspNetCore.Mvc;
using ProjectPlanner.Application.Services;
using ProjectPlanner.Application.Services.Requests;
using ProjectPlanner.Infrastructure.TaskObjects;

namespace Project_Planner.Controllers;

[ApiController]
[Microsoft.AspNetCore.Components.Route("api/CPM")]
public class CpmController
{
    private readonly CpmHandler _cpmHandler;

    public CpmController(CpmHandler cpmHandler)
    {
        _cpmHandler = cpmHandler;
    }

    [HttpPost]
    public async Task<IActionResult> PostCpmRequest([FromBody] CpmTask cpmTask)
    {
        var task = new CpmRequest(cpmTask);
        
        var solution = await _cpmHandler.Handle(task);
    }
}