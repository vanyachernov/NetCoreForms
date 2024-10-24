using CSharpFunctionalExtensions;
using Forms.Domain.Shared.IDs;

namespace Forms.Application.TemplateDir.AddQuestion;

public class AddQuestionHandler(ITemplatesRepository templateRepository)
{
    public async Task<Result<Guid>> Handle(
        Guid templateId,
        AddQuestionRequest request,
        CancellationToken cancellationToken = default)
    {
        return Result.Success(AnswerId.NewId.Value);
    }
}