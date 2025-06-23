using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ScheduleAI.Api.Schemas;
using ScheduleAI.Core.Abstractions;
using ScheduleAI.Core.Models;

namespace ScheduleAI.Api.Controllers;

[ApiController]
[Route("/api/v1/teachers")]
public class TeachersController(ITeachersService teachersService) : Controller
{
    [HttpGet]
    public async Task<ActionResult<ResponseSchema<Teacher[]>>> GetTeachers([FromQuery] [Required] string universityId,
        [FromQuery] string? search = null)
    {
        Teacher[] teachers;
        if (search == null)
            teachers = (await teachersService.GetTeachersAsync(universityId)).ToArray();
        else
            teachers = (await teachersService.GetTeachersAsync(universityId, search)).ToArray();

        return Ok(new ResponseSchema<Teacher[]>()
        {
            Detail = "Teachers was selected",
            Data = teachers
        });
    }

    [HttpGet("{teacherId}")]
    public async Task<ActionResult<ResponseSchema<Teacher>>> GetTeacherByName(
        [FromQuery] [Required] string universityId,
        string teacherId)
    {
        var teacher = await teachersService.GetTeacherByIdAsync(universityId, teacherId);

        return Ok(new ResponseSchema<Teacher>()
        {
            Detail = "Teacher was selected",
            Data = teacher
        });
    }
}