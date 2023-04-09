using System.Runtime.CompilerServices;
using Amazon.Lambda.Core;

namespace ProductCatalogue.Infrastructure.LambdaLogger;

public interface ILogger
{
    void LogError(string msg, [CallerMemberName] string caller = null);
    void LogInfo(string msg, [CallerMemberName] string caller = null);
    void LogWarning(string msg, [CallerMemberName] string caller = null);
    void SetLoggerContext(ILambdaLogger logger);
}
