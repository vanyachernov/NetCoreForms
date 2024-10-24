namespace Forms.API.Response;

public record ResponseError(string? ErrorCode, string? ErrorMessage, string? InvalidField);