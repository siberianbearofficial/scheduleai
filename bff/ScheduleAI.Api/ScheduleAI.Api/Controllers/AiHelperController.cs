using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ScheduleAI.Api.Schemas;
using ScheduleAI.Core.Abstractions;
using ScheduleAI.Core.Models;

namespace ScheduleAI.Api.Controllers;

[ApiController]
[Route("/api/v1/aiHelper")]
public class AiHelperController(IAiHelperService aiHelperService) : Controller
{
    [HttpPost]
    public async Task<ActionResult<ResponseSchema<IAiHelperTask>>> PostAiHelperTask(
        [FromBody] [Required] AiHelperRequestModel request)
    {
        var task = await aiHelperService.AskHelper(request.Prompt, request.UniversityId, request.GroupId);
        return Ok(new ResponseSchema<IAiHelperTask>
        {
            Detail = "AiHelper task was created.",
            Data = task,
        });
    }

    [HttpGet("{taskId:guid}")]
    public async Task<ActionResult<ResponseSchema<IAiHelperTask>>> GetAiHelperTask(Guid taskId)
    {
        var task = await aiHelperService.GetTask(taskId);
        return Ok(new ResponseSchema<IAiHelperTask>
        {
            Detail = "AiHelper task was selected.",
            Data = task,
        });
    }
}