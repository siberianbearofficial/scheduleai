using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ScheduleAI.Api.Schemas;
using ScheduleAI.Core.Models;

namespace ScheduleAI.Api.Controllers;

[ApiController]
[Route("/api/v1/mergedPairs")]
public class PairsController : Controller
{
    [HttpPost]
    public async Task<ActionResult<ResponseSchema<MergedPair[]>>> MergedPairs([FromBody] [Required] MergedPairsRequestSchema request)
    {
        throw new NotImplementedException();
    }
}