using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;


using BmstuSchedule.Client.Models;
using BmstuSchedule.Client.Exceptions;

namespace BmstuSchedule.Client;

public class BmstuScheduleClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public BmstuScheduleClient(string baseUrl)
    {
        _httpClient = new HttpClient() { BaseAddress = new Uri(baseUrl.TrimEnd('/')) };
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }

    #region teachers

    /// <summary>
    /// Get list of teachers 
    /// </summary>
    /// <param name="name">Filter teachers by any part of full name</param>
    /// <param name="department">Filter teachers by department abbreviation</param>
    /// <param name="page">Page number</param>
    /// <param name="size">Page size</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    public async Task<TeacherListResponse> GetTeachers(string? name = null, string? department = null, int page = 1, int size = 20, CancellationToken? cancellationToken = null)
    {
        Dictionary<string, string?> query = [];
        if (name != null) query["name"] = Convert.ToString(name);
        if (department != null) query["department"] = Convert.ToString(department);
        query["page"] = Convert.ToString(page);
        query["size"] = Convert.ToString(size);
        return await GetAsync<TeacherListResponse>($"/teachers", query, cancellationToken ?? CancellationToken.None);
    }

    /// <summary>
    /// Get a specific teacher by ID 
    /// </summary>
    /// <param name="teacherId">ID of the teacher</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    public async Task<TeacherResponse> GetTeacherById(int teacherId, CancellationToken? cancellationToken = null)
    {
        Dictionary<string, string?> query = [];
        
        return await GetAsync<TeacherResponse>($"/teachers/{teacherId}", query, cancellationToken ?? CancellationToken.None);
    }

    /// <summary>
    /// Get schedule for a specific teacher 
    /// </summary>
    /// <param name="teacherId">ID of the teacher</param>
    /// <param name="dtFrom">Start datetime</param>
    /// <param name="dtTo">End datetime</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    public async Task<TeacherScheduleResponse> GetTeacherSchedule(int teacherId, DateTime? dtFrom = null, DateTime? dtTo = null, CancellationToken? cancellationToken = null)
    {
        Dictionary<string, string?> query = [];
        if (dtFrom != null) query["dt_from"] = dtFrom.Value.ToString("o");
        if (dtTo != null) query["dt_to"] = dtTo.Value.ToString("o");
        return await GetAsync<TeacherScheduleResponse>($"/teachers/{teacherId}/schedule", query, cancellationToken ?? CancellationToken.None);
    }

    #endregion

    #region groups

    /// <summary>
    /// Get list of groups 
    /// </summary>
    /// <param name="abbr">Filter groups by abbreviation</param>
    /// <param name="course">Filter groups by course abbreviation</param>
    /// <param name="department">Filter groups by department abbreviation</param>
    /// <param name="faculty">Filter groups by faculty abbreviation</param>
    /// <param name="filial">Filter groups by filial abbreviation</param>
    /// <param name="page">Page number</param>
    /// <param name="size">Page size</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    public async Task<GroupListResponse> GetGroups(string? abbr = null, string? course = null, string? department = null, string? faculty = null, string? filial = null, int page = 1, int size = 20, CancellationToken? cancellationToken = null)
    {
        Dictionary<string, string?> query = [];
        if (abbr != null) query["abbr"] = Convert.ToString(abbr);
        if (course != null) query["course"] = Convert.ToString(course);
        if (department != null) query["department"] = Convert.ToString(department);
        if (faculty != null) query["faculty"] = Convert.ToString(faculty);
        if (filial != null) query["filial"] = Convert.ToString(filial);
        query["page"] = Convert.ToString(page);
        query["size"] = Convert.ToString(size);
        return await GetAsync<GroupListResponse>($"/groups", query, cancellationToken ?? CancellationToken.None);
    }

    /// <summary>
    /// Get a specific group by ID 
    /// </summary>
    /// <param name="groupId">ID of the group</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    public async Task<GroupResponse> GetGroupById(int groupId, CancellationToken? cancellationToken = null)
    {
        Dictionary<string, string?> query = [];
        
        return await GetAsync<GroupResponse>($"/groups/{groupId}", query, cancellationToken ?? CancellationToken.None);
    }

    /// <summary>
    /// Get schedule for a specific group 
    /// </summary>
    /// <param name="groupId">ID of the group</param>
    /// <param name="dtFrom">Start datetime</param>
    /// <param name="dtTo">End datetime</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    public async Task<GroupScheduleResponse> GetGroupSchedule(int groupId, DateTime dtFrom, DateTime dtTo, CancellationToken? cancellationToken = null)
    {
        Dictionary<string, string?> query = [];
        query["dt_from"] = dtFrom.ToString("o");
        query["dt_to"] = dtTo.ToString("o");
        return await GetAsync<GroupScheduleResponse>($"/groups/{groupId}/schedule", query, cancellationToken ?? CancellationToken.None);
    }

    #endregion

    #region rooms

    /// <summary>
    /// Get list of rooms 
    /// </summary>
    /// <param name="building">Filter rooms by building</param>
    /// <param name="page">Page number</param>
    /// <param name="size">Page size</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    public async Task<RoomListResponse> GetRooms(string? building = null, int page = 1, int size = 20, CancellationToken? cancellationToken = null)
    {
        Dictionary<string, string?> query = [];
        if (building != null) query["building"] = Convert.ToString(building);
        query["page"] = Convert.ToString(page);
        query["size"] = Convert.ToString(size);
        return await GetAsync<RoomListResponse>($"/rooms", query, cancellationToken ?? CancellationToken.None);
    }

    /// <summary>
    /// Get a specific room by ID 
    /// </summary>
    /// <param name="roomId">ID of the room</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    public async Task<RoomResponse> GetRoomById(int roomId, CancellationToken? cancellationToken = null)
    {
        Dictionary<string, string?> query = [];
        
        return await GetAsync<RoomResponse>($"/rooms/{roomId}", query, cancellationToken ?? CancellationToken.None);
    }

    /// <summary>
    /// Get schedule for a specific room 
    /// </summary>
    /// <param name="roomId">ID of the room</param>
    /// <param name="day">Day of week</param>
    /// <param name="dtFrom">Start datetime</param>
    /// <param name="dtTo">End datetime</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    public async Task<RoomScheduleResponse> GetRoomSchedule(int roomId, Models.DayOfWeek? day = null, DateTime? dtFrom = null, DateTime? dtTo = null, CancellationToken? cancellationToken = null)
    {
        Dictionary<string, string?> query = [];
        if (day != null) query["day"] = Convert.ToString(day);
        if (dtFrom != null) query["dt_from"] = dtFrom.Value.ToString("o");
        if (dtTo != null) query["dt_to"] = dtTo.Value.ToString("o");
        return await GetAsync<RoomScheduleResponse>($"/rooms/{roomId}/schedule", query, cancellationToken ?? CancellationToken.None);
    }

    /// <summary>
    /// Get list of rooms that are free during the specified time period 
    /// </summary>
    /// <param name="dtFrom">Start datetime</param>
    /// <param name="dtTo">End datetime</param>
    /// <param name="building">Filter rooms by building</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    public async Task<RoomListResponse> GetRoomsFree(DateTime dtFrom, DateTime dtTo, string? building = null, CancellationToken? cancellationToken = null)
    {
        Dictionary<string, string?> query = [];
        query["dt_from"] = dtFrom.ToString("o");
        query["dt_to"] = dtTo.ToString("o");
        if (building != null) query["building"] = Convert.ToString(building);
        return await GetAsync<RoomListResponse>($"/rooms/free", query, cancellationToken ?? CancellationToken.None);
    }

    #endregion


    #region Private helpers

    private static string GenerateUrl(string url, Dictionary<string, string?> queryParams)
    {
        return url + "?" + string.Join("&", queryParams.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value ?? "")}"));
    }

    private async Task<T> GetAsync<T>(string url, Dictionary<string, string?> query, CancellationToken cancellationToken)
    {
        HttpResponseMessage response;
        try
        {
            response = await _httpClient.GetAsync(GenerateUrl(url, query), cancellationToken);
        }
        catch (HttpRequestException e)
        {
            throw new BmstuScheduleConnectionException(e.Message, e);
        }

        if (!response.IsSuccessStatusCode)
        {
            throw response.StatusCode switch
            {
                HttpStatusCode.NotFound => new BmstuScheduleNotFoundException(),
                HttpStatusCode.InternalServerError => new BmstuScheduleInternalServerErrorException(),
                HttpStatusCode.BadRequest => new BmstuScheduleBadRequestException(),
                HttpStatusCode.Unauthorized => new BmstuScheduleUnauthorizedException(),
                HttpStatusCode.BadGateway => new BmstuScheduleBadGatewayException(),
                HttpStatusCode.RequestTimeout => new BmstuScheduleRequestTimeoutException(),
                HttpStatusCode.NotAcceptable => new BmstuScheduleNotAcceptableException(),
                HttpStatusCode.NotImplemented => new BmstuScheduleNotImplementedException(),
                HttpStatusCode.MethodNotAllowed => new BmstuScheduleMethodNotAllowedException(),
                HttpStatusCode.Conflict => new BmstuScheduleConflictException(),
                _ => new BmstuScheduleErrorCodeException((int)response.StatusCode),
            };
        }

        var result = await response.Content.ReadFromJsonAsync<T>(_jsonOptions, cancellationToken);
        if (result == null)
        {
            throw new BmstuScheduleJsonException($"Failed to deserialize {typeof(T).Name}");
        }
        return result;
    }

    #endregion
}