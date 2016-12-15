using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chm.Logging.Helper;

namespace Chm.Logging
{
    public class JobLoggerConfig: IJobLoggerConfig
    {
        public bool IsLogToFile => AppSettingsHelper.Get<bool>("IsLogToFile");

        public bool IsLogToDataBase => AppSettingsHelper.Get<bool>("IsLogToDataBase");

        public bool IsLogToConsole => AppSettingsHelper.Get<bool>("IsLogToConsole");

        public string DbConnectionString => AppSettingsHelper.Get<string>("DbConnectionString");

        public string FilePath => AppSettingsHelper.Get<string>("FilePath");

        public int LogSeverity => AppSettingsHelper.Get<int>("LogSeverity");
    }
}
