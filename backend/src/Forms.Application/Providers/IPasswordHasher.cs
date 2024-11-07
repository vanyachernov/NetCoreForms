namespace Forms.Application.Providers;

public interface IPasswordHasher
{
    string Generate(int length);
}