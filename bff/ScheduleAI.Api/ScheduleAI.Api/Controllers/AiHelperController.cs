using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ScheduleAI.Api.Schemas;

namespace ScheduleAI.Api.Controllers;

[ApiController]
[Route("/api/v1/aiHelper")]
public class AiHelperController : Controller
{
    [HttpPost]
    public async Task<ActionResult<ResponseSchema<AiHelperResponseModel>>> PostAiHelperPrompt(
        [FromBody] [Required] AiHelperRequestModel request)
    {
        throw new NotImplementedException();
    }
}