using CSharpFunctionalExtensions;

namespace Forms.Application.Template.GetUsers;

public class GetUsersHandler(ITemplateRepository templateRepository)
{
    public async Task<Result<IEnumerable<GetUsersResponse>>> Handle(
        CancellationToken cancellationToken = default)
    {
        var getUsersResult = await templateRepository.GetUsers(cancellationToken);

        return getUsersResult.IsFailure 
            ? Result.Failure<IEnumerable<GetUsersResponse>>(getUsersResult.Error) 
            : Result.Success(getUsersResult.Value);
    }
}