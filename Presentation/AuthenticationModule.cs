using Application.Authentication.Abstraction;
using Application.User.Command.Create;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Presentation.Model;

namespace Presentation;

public class AuthenticationModule() : CarterModule("api/auth")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", Login);
        app.MapPost("/register", Register);
    }

    [AllowAnonymous]
    private static async Task<IResult> Login([FromBody] LoginRequest request, [FromServices] IAuthenticationService authenticationService, CancellationToken cancellationToken)
    {
        var result = await authenticationService.Login(request.Email,request.Password,cancellationToken);
        if (result.IsFailure)
        {
            return Results.BadRequest(result.Error.Name);
        }
        return Results.Ok(result.Value);
    }
    
    [AllowAnonymous]
    private static async Task<IResult> Register([FromBody] RegisterRequest request, [FromServices] ISender sender, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new CreateUserCommand(request.Email,request.Password), cancellationToken);
        if (result.IsFailure)
        {
            return Results.BadRequest(result.Error.Name);
        }
        return Results.Ok();
    }
}