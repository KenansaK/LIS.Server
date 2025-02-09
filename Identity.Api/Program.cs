using Identity.API.Configuration;
using Identity.Application.Services;
using Kernal.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services
    .RegisterPersistencfe()
    .AddApplication()
    .AddAuthenticationAndAuthorization(builder.Configuration)
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

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRoleService, RoleService>();

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
