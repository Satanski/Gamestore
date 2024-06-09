using System.Text;

namespace Gamestore.WebApi.Helpers;

internal static class ExceptionLogHelpers
{
    internal static void LogException(this ILogger logger, Exception ex)
    {
        var sb = new StringBuilder();

        while (ex != null)
        {
            sb.Append($"{ex} \n {ex.Message} \n {ex.StackTrace} \n {ex.HelpLink} \n");
            ex = ex.InnerException;
        }

        logger.LogError("{Exception}", sb.ToString());
    }
}
