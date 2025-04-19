﻿namespace ScheduleAI.Api.Schemas;

public class ResponseSchema<T>
{
    public required T Data { get; init; }
    public string Detail { get; init; } = string.Empty;
}