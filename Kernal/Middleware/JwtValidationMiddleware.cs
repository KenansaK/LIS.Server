using Kernal.Interfaces;
using Kernal.Jwt;
using Kernal.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<JwtValidationMiddleware> _logger;
    private readonly JwtOptions _jwtOptions;

    public JwtValidationMiddleware(
        RequestDelegate next, // Automatically passed by the middleware pipeline
        ILogger<JwtValidationMiddleware> logger,
        IOptions<JwtOptions> jwtOptions)
    {
        _next = next; // This is the next middleware in the pipeline
        _logger = logger;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task InvokeAsync(HttpContext context, IRoleService roleService)
    {
        // Your token validation logic here...
        if (context.GetEndpoint()?.Metadata?.OfType<AllowAnonymousAttribute>().Any() == true)
        {
            await _next(context);

            return;
        }

        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        if (authHeader == null || !authHeader.StartsWith("Bearer "))
        {
            _logger.LogWarning("Authorization header missing or invalid.");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Authorization header missing or invalid");
            return;
        }

        var token = authHeader.Substring("Bearer ".Length).Trim();
        try
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
            var handler = new JwtSecurityTokenHandler();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtOptions.Issuer,
                ValidAudience = _jwtOptions.Audience,
                IssuerSigningKey = key
            };

            var principal = handler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
            context.User = principal;

            var endpoint = context.GetEndpoint();
            var requiredRoles = endpoint?.Metadata.GetMetadata<AuthorizeAttribute>()?.Roles;

            if (!string.IsNullOrEmpty(requiredRoles))
            {
                var userRoles = principal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
                var requiredRolesArray = requiredRoles.Split(',');

                if (!requiredRolesArray.Any(role => userRoles.Contains(role)))
                {
                    _logger.LogWarning("User does not have the required role.");
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Forbidden: You do not have the required role.");
                    return;
                }
            }

            var requiredPermission = endpoint?.Metadata.GetMetadata<PermissionAttribute>()?.Permission;

            if (!string.IsNullOrEmpty(requiredPermission))
            {
                var userId = int.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var userPermissions = await roleService.GetUserPermissionsAsync(userId);

                if (!userPermissions.Contains(requiredPermission))
                {
                    _logger.LogWarning("User does not have the required permission.");
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Forbidden: You do not have the required permission.");
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Token validation failed: {ex.Message}");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        await _next(context);
    }
}
