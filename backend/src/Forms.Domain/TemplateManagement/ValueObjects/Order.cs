using CSharpFunctionalExtensions;

namespace Forms.Domain.TemplateManagement.ValueObjects;

public record Order
{
    private Order(int value)
    {
        Value = value;
    }

    public int Value { get; } = default!;

    public static Result<Order> Create(int order)
    {
        return new Order(order);
    }
};