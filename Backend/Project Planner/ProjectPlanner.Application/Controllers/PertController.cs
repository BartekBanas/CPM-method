using Microsoft.AspNetCore.Mvc;
using ProjectPlanner.Application.Services;

namespace Project_Planner.Controllers;

[ApiController]
[Microsoft.AspNetCore.Components.Route("api/PERT")]
public class PertController
{
    private readonly PertTaskService _pertTaskService;
}