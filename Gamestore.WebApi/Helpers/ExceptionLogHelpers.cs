using System.Text;

namespace Gamestore.WebApi.Helpers;

internal static class ExceptionLogHelpers
{
    internal static string CreateLogMessage(Exception ex)
    {
        StringBuilder sb = new StringBuilder();

        while (ex != null)
        {
            sb.Append($"{ex} \n {ex.Message} \n {ex.StackTrace} \n {ex.HelpLink} \n");
            ex = ex.InnerException;
        }

        return sb.ToString();
    }
}
