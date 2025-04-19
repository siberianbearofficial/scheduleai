using ScheduleAI.Application.Services;
using ScheduleAI.Core.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<IUniversityService, UniversityService>();
builder.Services.AddScoped<IGroupsService, GroupsService>();
builder.Services.AddScoped<ITeachersService, TeachersService>();
builder.Services.AddScoped<IScheduleService, PairsService>();
builder.Services.AddScoped<IAiHelperService, AiHelperService>();

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();