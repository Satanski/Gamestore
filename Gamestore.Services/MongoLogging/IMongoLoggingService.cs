using Gamestore.BLL.Models;
using Gamestore.Services.Models;

namespace Gamestore.BLL.MongoLogging;

public interface IMongoLoggingService
{
    Task LogGameAddAsync(GameDtoWrapper value);

    Task LogGameDeleteAsync(Guid gameId);

    Task LogGameUpdateAsync(GameModelDto oldValue, GameDtoWrapper newValue);
}
