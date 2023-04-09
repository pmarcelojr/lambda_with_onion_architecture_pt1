using System.Runtime.CompilerServices;
using Amazon.Lambda.Core;
using Ardalis.GuardClauses;

namespace ProductCatalogue.Infrastructure.LambdaLogger;

public class Logger : ILogger
{
    private ILambdaLogger _logger;

    public void SetLoggerContext(ILambdaLogger logger)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
    }

    private void WriteLog(string msg)
    {
        if (_logger != null)
            _logger.Log(msg);
        else
            System.Diagnostics.Debug.WriteLine(msg);
    }

    public void LogError(string msg, [CallerMemberName] string caller = null)
    {
        WriteLog($"[error]: [caller: {caller}] - {msg}");
    }

    public void LogInfo(string msg, [CallerMemberName] string caller = null)
    {
        WriteLog($"[info]: [caller: {caller}] - {msg}");
    }

    public void LogWarning(string msg, [CallerMemberName] string caller = null)
    {
        WriteLog($"[warning]: [caller: {caller}] - {msg}");
    }
}
