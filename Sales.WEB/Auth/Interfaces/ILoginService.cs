namespace Sales.WEB.Auth.Interfaces
{
    public interface ILoginService
    {
        Task LoginAsync(string token);

        Task LogoutAsync();
    }
}
