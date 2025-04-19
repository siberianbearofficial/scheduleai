using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ScheduleAI.Api.Schemas;
using ScheduleAI.Core.Abstractions;
using ScheduleAI.Core.Models;

namespace ScheduleAI.Api.Controllers;

[ApiController]
[Route("/api/v1/mergedPairs")]
public class PairsController(IScheduleService scheduleService) : Controller
{
    [HttpPost]
    public async Task<ActionResult<ResponseSchema<MergedPair[]>>> MergedPairs(
        [FromBody] [Required] MergedPairsRequestSchema request)
    {
        var mergedPairs = await scheduleService.GetMergedScheduleAsync(request.UniversityId, request.GroupId,
            request.TeacherId, request.StartTime, request.EndTime);
        return Ok(new ResponseSchema<MergedPair[]>
        {
            Detail = "Pairs was merged",
            Data = mergedPairs.ToArray()
        });
    }
}