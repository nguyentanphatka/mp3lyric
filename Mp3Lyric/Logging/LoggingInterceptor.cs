using Castle.DynamicProxy;

namespace Mp3Lyric.Logging;

public class LoggingInterceptor : IInterceptor
{
    private readonly Serilog.ILogger _serilogLogger;
    //private readonly Castle.Core.Logging.ILogger _castleLogger;

    public LoggingInterceptor(Serilog.ILogger serilogLogger, Castle.Core.Logging.ILogger castleLogger)
    {
        _serilogLogger = serilogLogger;
        //_castleLogger = castleLogger;
    }

    public void Intercept(IInvocation invocation)
    {
        var methodName = $"{invocation.TargetType.Name}.{invocation.Method.Name}";

        // Ghi log tên method bằng Castle.Core.Logging
        //_castleLogger.DebugFormat("Method: {0}", methodName);

        // Ghi log bằng Serilog

        _serilogLogger.Information($"Method: {methodName}");

        invocation.Proceed();
    }
}