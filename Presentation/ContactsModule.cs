using Application.Contacts.Command.Create;
using Application.Contacts.Command.Delete;
using Application.Contacts.Command.Update;
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
        app.MapPut("/{id:guid}", Update);
        app.MapDelete("/{id:guid}", Delete);
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
    [Authorize]
    private static async Task<IResult> Update([FromRoute] Guid id,[FromBody] UpdateContactRequest request, [FromServices] ISender sender)
    {
        var command = new UpdateContactCommand(id,request.Name, request.Surname, request.Email, request.CategoryName,
            request.SubcategoryName, request.BirthDate, request.Password, request.PhoneNumber);
        var result = await sender.Send(command);
        if (result.IsFailure)
        {
            return Results.BadRequest(result.Error.Name);
        }

        return Results.Ok();
    }
    [Authorize]
    private static async Task<IResult> Delete([FromRoute] Guid id, [FromServices] ISender sender)
    {
        var command = new DeleteContactCommand(id);
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