using Microsoft.Extensions.Logging;

namespace Customers.Service.Utils
{
  public static class LoggerUtils
  {
    public static void LogException(this ILogger logger, string MethodName, string ExceptionMessage, string ExceptionStackTrace) => logger.LogError("{MethodName} - Exception: {ExceptionMessage}. \n Stack Trace: {ExceptionStackTrace}"
        , MethodName, ExceptionMessage, ExceptionStackTrace);
  }
}
