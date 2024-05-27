using Microsoft.Extensions.DependencyInjection;

namespace Gamestore.DAL.Interfaces;

public interface IInstaller
{
    public int Order => -1;

    void AddServices(IServiceCollection services);
}
