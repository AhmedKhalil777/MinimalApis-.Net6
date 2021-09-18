using Microsoft.AspNetCore.Mvc;
using Minimal.Apis;
using Minimal.Apis.Repositories;

// ---Ahmed Khalil ---
// The default implementation of dotnet 6 apps create with {Top level statement mode}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new() { Title = "Minimal.Apis", Version = "v1" }); });
builder.Services.AddSingleton<EmployeesRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal.Apis v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapGet("/employees", ([FromServices] EmployeesRepository repo)=>
{
    return repo.GetEmployees();
});

app.MapGet("/employees/{empId}", ([FromServices] EmployeesRepository repo,[FromRoute] Guid empId) =>
{
    return repo.GetEmployee(empId);
});

app.MapPut("/employees", ([FromServices] EmployeesRepository repo, [FromBody] Employee employee) =>
{
     repo.UpdateEmployee(employee);
});

app.MapPost("/employees", ([FromServices] EmployeesRepository repo, [FromForm] Employee employee) =>
{
    repo.AddEmployee(employee);
});

app.MapDelete("/employees/{empId}", ([FromServices] EmployeesRepository repo, [FromRoute] Guid empId) =>
{
    repo.DeleteEmployee(empId);
});
app.Run();