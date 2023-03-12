using Microsoft.AspNetCore.Mvc;
using ProjectPlanner.Application.Services;

namespace Project_Planner.Controllers;

[ApiController]
[Microsoft.AspNetCore.Components.Route("api/CPM")]
public class CpmController
{
    private readonly CpmHandler _cpmHandler;
}