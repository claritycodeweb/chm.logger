using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chm.Logging
{
    public enum LogLevel
    {
        Info,
        Warn,
        Err
    }

    public interface IJobLogger : IDisposable
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogError(string message);
    }
}
