using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ScheduleAI.Api.Schemas;
using ScheduleAI.Core.Models;

namespace ScheduleAI.Api.Controllers;

[ApiController]
[Route("/api/v1/universities")]
public class UniversitiesController : Controller
{
    [HttpGet]
    public async Task<ActionResult<ResponseSchema<University>>> GetUniversities()
    {
        throw new NotImplementedException();
    }
}