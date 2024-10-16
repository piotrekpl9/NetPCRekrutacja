using Application.Authentication.Abstraction;
using Application.User.Command.Create;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation;

public class AuthenticationModule() : CarterModule("api/auth")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", Login);
        app.MapPost("/register", Register);
    }

    private static async Task<IResult> Login([FromBody] LoginRequest request, [FromServices] IAuthenticationService authenticationService, CancellationToken cancellationToken)
    {
        var result = await authenticationService.Login(request.Email,request.Password,cancellationToken);
        if (result.IsFailure)
        {
            return Results.BadRequest();
        }
        return Results.Ok(result.Value);
    }

    private static async Task<IResult> Register([FromBody] RegisterRequest request, [FromServices] ISender sender, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new CreateUserCommand(request.Email,request.Password), cancellationToken);
        if (result.IsFailure)
        {
            return Results.BadRequest();
        }
        return Results.Ok();
    }
}