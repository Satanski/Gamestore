using Gamestore.BLL.BanHandler;

namespace Gamestore.BLL.Filtering;

public class FilterServiceBuilder : IFilterServiceBuilder
{
    private IFilterHandler _firstHandler;
    private IFilterHandler _lastHandler;

    public FilterServiceBuilder AddHandler(IFilterHandler handler)
    {
        if (_firstHandler == null)
        {
            _firstHandler = handler;
            _lastHandler = handler;
        }
        else
        {
            _lastHandler.SetNext(handler);
            _lastHandler = handler;
        }

        return this;
    }

    public FilterService Build()
    {
        return new FilterService(_firstHandler);
    }
}
