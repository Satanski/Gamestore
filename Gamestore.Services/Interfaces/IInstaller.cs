using Microsoft.Extensions.DependencyInjection;

namespace Gamestore.BLL.Interfaces;

public interface IInstaller
{
    public int Order => -1;

    void AddServices(IServiceCollection services);
}
