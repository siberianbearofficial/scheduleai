using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using AiHelper.Client.Models;
using AiHelper.Client.Exceptions;
using Newtonsoft.Json;

namespace AiHelper.Client;

public class AiHelperClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public AiHelperClient(string baseUrl)
    {
        _httpClient = new HttpClient() { BaseAddress = new Uri(baseUrl.TrimEnd('/')) };
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };
    }

    #region agent

    /// <summary>
    /// Agent Request 
    /// </summary>
    /// <param name="agentRequest">Request body</param>
    /// <param name="university">Университет студента</param>
    /// <param name="group">Группа студента</param>
    /// <param name="userMessage">Сообщение от пользователя. None если endpoint вызывается для передачи агенту результатов выполнения функций. Не может быть None одновременно с messages!</param>
    /// <param name="user">Можно передать имя пользователя</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    public async Task<AgentResponseModel> PostApiAgentRequest(AgentRequestModel? agentRequest, string university, string group,
        string? userMessage = null, string? user = null, CancellationToken? cancellationToken = null)
    {
        Dictionary<string, string?> query = [];
        query["university"] = Convert.ToString(university);
        query["group"] = Convert.ToString(group);
        if (userMessage != null) query["user_message"] = Convert.ToString(userMessage);
        if (user != null) query["user"] = Convert.ToString(user);
        return await PostAsync<AgentResponseModel>($"/api/agent/request", agentRequest, query,
            cancellationToken ?? CancellationToken.None);
    }

    /// <summary>
    /// Add Function Results 
    /// </summary>
    /// <param name="toolCallId">id вызванной функции из messages.tool_calls</param>
    /// <param name="functionResult">request body</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    public async Task<AgentResponseModel> PostApiAgentAddFunctionResults(FunctionResultsModel functionResult, string toolCallId,
        CancellationToken? cancellationToken = null)
    {
        Dictionary<string, string?> query = [];
        query["tool_call_id"] = Convert.ToString(toolCallId);
        return await PostAsync<AgentResponseModel>($"/api/agent/add-function-results", functionResult, query,
            cancellationToken ?? CancellationToken.None);
    }

    #endregion


    #region Private helpers

    private static string GenerateUrl(string url, Dictionary<string, string?> queryParams)
    {
        return url + "?" + string.Join("&", queryParams.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value ?? "")}"));
    }

    private async Task<T> PostAsync<T>(string url, object? body, Dictionary<string, string?> query,
        CancellationToken cancellationToken)
    {
        HttpResponseMessage response;
        try
        {
            response = await _httpClient.PostAsync(GenerateUrl(url, query), JsonContent.Create(body, options: _jsonOptions),
                cancellationToken);
        }
        catch (HttpRequestException e)
        {
            throw new AiHelperConnectionException(e.Message, e);
        }

        if (!response.IsSuccessStatusCode)
        {
            throw response.StatusCode switch
            {
                HttpStatusCode.NotFound => new AiHelperNotFoundException(),
                HttpStatusCode.InternalServerError => new AiHelperInternalServerErrorException(),
                HttpStatusCode.BadRequest => new AiHelperBadRequestException(),
                HttpStatusCode.Unauthorized => new AiHelperUnauthorizedException(),
                HttpStatusCode.BadGateway => new AiHelperBadGatewayException(),
                HttpStatusCode.RequestTimeout => new AiHelperRequestTimeoutException(),
                HttpStatusCode.NotAcceptable => new AiHelperNotAcceptableException(),
                HttpStatusCode.NotImplemented => new AiHelperNotImplementedException(),
                HttpStatusCode.MethodNotAllowed => new AiHelperMethodNotAllowedException(),
                HttpStatusCode.Conflict => new AiHelperConflictException(),
                _ => new AiHelperErrorCodeException((int)response.StatusCode),
            };
        }

        var result = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync(cancellationToken));
        if (result == null)
        {
            throw new AiHelperJsonException($"Failed to deserialize {typeof(T).Name}");
        }

        return result;
    }

    #endregion
}