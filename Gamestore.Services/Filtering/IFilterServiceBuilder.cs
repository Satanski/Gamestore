using Gamestore.BLL.BanHandler;

namespace Gamestore.BLL.Filtering;

public interface IFilterServiceBuilder
{
    FilterServiceBuilder AddHandler(IFilterHandler handler);

    FilterService Build();
}
