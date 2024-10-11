using CSharpFunctionalExtensions;
using Forms.Domain.Shared;

namespace Forms.Domain.TemplateManagement.ValueObjects;

public record Option
{
    private Option(Guid id) => Id = id;
    
    public Guid Id { get; }

    public static Result<Option, Error> Create(Guid id)
    {
        return new Option(id);
    }
};