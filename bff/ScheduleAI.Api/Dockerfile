FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

WORKDIR "/src/ScheduleAI.Web"
RUN dotnet build "ScheduleAI.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ScheduleAI.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_HTTP_PORTS=8081;3000
EXPOSE 3000
