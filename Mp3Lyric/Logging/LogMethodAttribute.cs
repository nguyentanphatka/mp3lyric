using Castle.DynamicProxy;
using Serilog;

namespace Mp3Lyric.Logging
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class LogMethodAttribute : Attribute
    {
    }  
    public class LogAttribute : Attribute
    {
    }
}
