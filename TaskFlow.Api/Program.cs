using MediatR;
using TaskFlow.BuildingBlocks;
using TaskFlow.BuildingBlocks.Application.Behaviors;
using TaskFlow.BuildingBlocks.Presentation.Middleware;
using TaskFlow.Modules.Users.Application;
using TaskFlow.Modules.Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});


// Registers Users module application layer services:
// - MediatR handlers
// - FluentValidation validators
// - CQRS-related application services
//
// Assembly scanning automatically discovers handlers
// and validators from the Users module.
builder.Services.AddUsersApplication();

// Registers IUserRepository and other infrastructure services
builder.Services.AddUsersInfrastructure(builder.Configuration);

// ------------------------------
// Register MediatR (Application Layer)
// ------------------------------
// Scans the Users Application assembly and registers all
// IRequestHandler implementations (e.g., GetUsersHandler, CreateUserHandler)

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);
});

builder.Services.AddBuildingBlocks(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors("AllowAll");

// ------------------------------
// Configure Middleware Pipeline
// ------------------------------
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();
app.Run();
