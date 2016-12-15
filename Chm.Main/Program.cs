using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chm.Logging;

namespace Chm.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            IJobLoggerConfig config = new JobLoggerConfig(); 
            IJobLogger logger = new JobLogger(config);

            logger.LogError("Test error");
            logger.LogInfo("Test info");
            logger.LogWarn("Test warning");
        }
    }
}
