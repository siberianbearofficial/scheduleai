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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.SetIsOriginAllowed(origin =>
                origin == "http://localhost:4200" || origin == "https://scheduleai-ui.netlify.app" ||
                origin.StartsWith("https://deploy-preview-") && origin.EndsWith("--scheduleai-ui.netlify.app"))
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();