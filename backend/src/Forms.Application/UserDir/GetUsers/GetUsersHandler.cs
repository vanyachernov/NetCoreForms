using CSharpFunctionalExtensions;

namespace Forms.Application.UserDir.GetUsers;

public class GetUsersHandler(IUsersRepository userRepository)
{
    public async Task<Result<IEnumerable<GetUsersResponse>>> Handle(
        CancellationToken cancellationToken = default)
    {
        var getUsersResult = await userRepository.GetUsers(cancellationToken);

        return getUsersResult.IsFailure 
            ? Result.Failure<IEnumerable<GetUsersResponse>>(getUsersResult.Error) 
            : Result.Success(getUsersResult.Value);
    }
}