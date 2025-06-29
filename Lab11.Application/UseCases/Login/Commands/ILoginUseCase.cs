namespace Lab11.Domain.Interfaces.IServices;

public interface ILoginUseCase
{
    Task<string> LoginAsync(string username, string password);
}