using Application.Contacts.Command.Create;
using Application.Contacts.Query.GetAll;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Presentation.Model.Request;

namespace Presentation;

public class ContactsModule() : CarterModule("/api/contacts")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/", Create);
        app.MapGet("/", GetAll);
    }

    [Authorize]
    private static async Task<IResult> Create([FromBody] CreateContactRequest request, [FromServices] ISender sender)
    {
        var command = new CreateContactCommand(request.Name, request.Surname, request.Email, request.CategoryName,
            request.SubcategoryName, request.BirthDate, request.Password, request.PhoneNumber);
        var result = await sender.Send(command);
        if (result.IsFailure)
        {
            return Results.BadRequest(result.Error.Name);
        }

        return Results.Ok();
    }
    
    private static async Task<IResult> GetAll([FromServices] ISender sender)
    {
        var command = new GetAllContactsQuery();
        var result = await sender.Send(command);
        if (result.IsFailure)
        {
            return Results.BadRequest(result.Error.Name);
        }

        return Results.Ok(result.Value);
    }
}