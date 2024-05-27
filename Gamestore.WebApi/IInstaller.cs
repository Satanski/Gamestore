namespace Gamestore.WebApi;

public interface IInstaller
{
    public int Order => -1;

    void AddServices(IServiceCollection services);
}
