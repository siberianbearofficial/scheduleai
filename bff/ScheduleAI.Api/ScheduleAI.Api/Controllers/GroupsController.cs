using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ScheduleAI.Api.Schemas;
using ScheduleAI.Core.Abstractions;
using ScheduleAI.Core.Models;

namespace ScheduleAI.Api.Controllers;

[ApiController]
[Route("/api/v1/groups")]
public class GroupsController(IGroupsService groupsService) : Controller
{
    [HttpGet]
    public async Task<ActionResult<ResponseSchema<Group[]>>> GetGroups([FromQuery] [Required] string universityId,
        [FromQuery] string? search = null)
    {
        Group[] groups;
        if (search == null)
            groups = (await groupsService.GetGroupsAsync(universityId)).ToArray();
        else
            groups = (await groupsService.GetGroupsAsync(universityId, search)).ToArray();

        return Ok(new ResponseSchema<Group[]>()
        {
            Detail = "groups was selected",
            Data = groups
        });
    }
}