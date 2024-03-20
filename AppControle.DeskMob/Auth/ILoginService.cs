
namespace AppControle.Hybrid.Auth
{
    public interface ILoginService
    {
        Task LoginAsync(string toke1n, bool remember);

        Task LogoutAsync();
    }
}
