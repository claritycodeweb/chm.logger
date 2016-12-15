using System;
using System.IO;
using Chm.Logging;
using Chm.Test.Logging.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
//using Chm.Test.Mock;

namespace Chm.Test.Logging
{

    [TestClass]
    public class JobLoggerTest
    {
        private IJobLogger _logger;
        private IJobLoggerConfig _config;

        [TestInitialize]
        public void SetupSwiperTests()
        {
            _config = new MockJobLoggerConfig();
            _logger = new JobLogger(_config);
        }

        [TestMethod]
        public void Test_Log_Write_To_File()
        {
            string path = $"{_config.FilePath}LogFile_{DateTime.Now.ToShortDateString()}.txt";

            CleanTestFolder();

            _logger.LogInfo("Test 123");

            Assert.IsTrue(File.ReadAllText(path).Contains("Test 123"));
        }

        /// <summary>
        /// Stop service started during class initialize and kill the thread
        /// </summary>
        [TestCleanup]
        public void CleanupSwiperTests()
        {
            _config = null;
            _logger = null;
        }


        private void  CleanTestFolder()
        {
            DirectoryInfo di = new DirectoryInfo(_config.FilePath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }
    }
}
