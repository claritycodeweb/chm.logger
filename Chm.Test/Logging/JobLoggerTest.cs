using Chm.Logging;
using Chm.Test.Logging.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Chm.Test.Mock;

namespace Chm.Test.Logging
{

    [TestClass]
    public class JobLoggerTest
    {
        private IJobLogger _logger;
        private IJobLoggerConfig _config;

        [ClassInitialize]
        public void SetupSwiperTests()
        {
            _config = new MockJobLoggerConfig();
            _logger = new JobLogger(_config);
        }

        /// <summary>
        /// Stop service started during class initialize and kill the thread
        /// </summary>
        [ClassCleanup]
        public void CleanupSwiperTests()
        {
            
        }


        [TestMethod]
        public void TestMethod1()
        {
            _logger.LogInfo("Test Message");
        }
    }
}
