using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ScheduleAI.Api.Schemas;
using ScheduleAI.Core.Abstractions;

namespace ScheduleAI.Api.Controllers;

[ApiController]
[Route("/api/v1/aiHelper")]
public class AiHelperController(IAiHelperService aiHelperService) : Controller
{
    [HttpPost]
    public async Task<ActionResult<ResponseSchema<AiHelperResponseModel>>> PostAiHelperPrompt(
        [FromBody] [Required] AiHelperRequestModel request)
    {
        var answer = await aiHelperService.AskHelper(request.Prompt, request.UniversityId, request.GroupId);
        return Ok(new ResponseSchema<AiHelperResponseModel>
        {
            Detail = "Answer from AiHelper",
            Data = new AiHelperResponseModel()
            {
                Answer = answer,
            }
        });
    }
}