using Microsoft.AspNetCore.Mvc;
using ProjectPlanner.Application.Services;

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
}