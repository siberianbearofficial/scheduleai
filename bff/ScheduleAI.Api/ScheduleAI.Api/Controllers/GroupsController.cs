using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ScheduleAI.Api.Schemas;
using ScheduleAI.Core.Models;

namespace ScheduleAI.Api.Controllers;

[ApiController]
[Route("/api/v1/groups")]
public class GroupsController : Controller
{
    [HttpGet]
    public async Task<ActionResult<ResponseSchema<Group>>> GetGroups([FromQuery] [Required] Guid universityId,
        [FromQuery] string? search = null)
    {
        throw new NotImplementedException();
    }
}