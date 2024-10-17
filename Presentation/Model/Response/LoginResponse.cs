namespace Presentation.Model.Response;

public record LoginResponse(string Token, Guid UserId);