using CRM.API.Configuration;
using CRM.Application.Behaviors;
using CRM.Application.Validators;
using FluentValidation;
using Kernal.Interfaces;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services
    .RegisterPersistencfe()
    .AddApplication()
    .AddAuthenticationAndAuthorization(builder.Configuration)
    .AddCaching(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddMessageBroker(builder.Configuration)
    .AddSwagger();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins); // Ensure this is after UseAuthentication
app.UseMiddleware<CustomExceptionHandlerMiddleware>();
app.UseMiddleware<JwtValidationMiddleware>();
app.MapControllers();
app.Run();