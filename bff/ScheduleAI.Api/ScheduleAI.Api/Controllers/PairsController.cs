using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ScheduleAI.Api.Schemas;
using ScheduleAI.Core.Models;

namespace ScheduleAI.Api.Controllers;

[ApiController]
[Route("/api/v1/pairs")]
public class PairsController : Controller
{
    [HttpGet]
    public async Task<ActionResult<ResponseSchema<Group>>> GetPairs([FromQuery] [Required] Guid universityId,
        [FromQuery] [Required] string groupId, [FromQuery] [Required] string teacherId,
        [FromQuery] [Required] DateTime startTime, [FromQuery] [Required] DateTime endTime)
    {
        throw new NotImplementedException();
    }
}