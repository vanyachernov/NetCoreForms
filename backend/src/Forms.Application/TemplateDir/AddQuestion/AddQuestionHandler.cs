using CSharpFunctionalExtensions;
using Forms.Domain.Shared.IDs;
using Forms.Domain.TemplateManagement.Entities;

namespace Forms.Application.TemplateDir.AddQuestion;

public class AddQuestionHandler(ITemplatesRepository templateRepository)
{
    public async Task<Result<Guid>> Handle(
        AddQuestionRequest request,
        CancellationToken cancellationToken = default)
    {
        // Создаем новый ответ
        // var answerResult = Answer.Create(
        //     AnswerId.NewId,
        //     request.Instance 
        //     request.Answer.Value,
        //     request.IsCorrect.Value
        // );
        
        // var adsad = Answer.Create()
        //
        // if (answerResult.IsFailure)
        //     return Result.Failure<Guid>(answerResult.Error);
        //
        // // Добавляем ответ к вопросу
        // question.AddAnswer(answerResult.Value);
        //
        // // Сохраняем изменения в репозитории
        // await _templateRepository.UpdateQuestion(question, cancellationToken);
        //
        // // Возвращаем ID добавленного ответа
        // return Result.Success(answerResult.Value.Id);

        return Result.Success(AnswerId.NewId.Value);
    }
}