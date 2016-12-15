using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Chm.Logging
{
    public interface IJobLoggerConfig
    {
        bool IsLogToFile { get; }

        bool IsLogToDataBase { get; }

        bool IsLogToConsole { get; }

        string DbConnectionString { get; }

        string FilePath { get; }

        int LogSeverity { get; }

    }
}
