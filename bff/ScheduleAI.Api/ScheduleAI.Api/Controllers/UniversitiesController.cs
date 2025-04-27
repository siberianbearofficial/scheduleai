using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ScheduleAI.Api.Schemas;
using ScheduleAI.Core.Abstractions;
using ScheduleAI.Core.Abstractions.Universities;
using ScheduleAI.Core.Models;

namespace ScheduleAI.Api.Controllers;

[ApiController]
[Route("/api/v1/universities")]
public class UniversitiesController(IUniversityService universityService) : Controller
{
    [HttpGet]
    public async Task<ActionResult<ResponseSchema<IUniversity[]>>> GetUniversities()
    {
        var universities = universityService.GetAllUniversities().ToArray();

        return Ok(new ResponseSchema<IUniversity[]>()
        {
            Detail = "groups was selected",
            Data = universities
        });
    }
}