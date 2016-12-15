using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chm.Logging;

namespace Chm.Test.Logging.Mock
{
    public class MockJobLoggerConfig : IJobLoggerConfig
    {
        public bool IsLogToFile => true;
        public bool IsLogToDataBase => true;
        public bool IsLogToConsole => true;
        public string DbConnectionString => "Data Source=(LocalDb);Initial Catalog=MyAppDB;Integrated Security=True";
        public string FilePath => @"C:\work\test\";
        public int LogSeverity => 0;
    }
}
