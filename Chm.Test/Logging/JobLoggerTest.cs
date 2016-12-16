using System;
using System.IO;
using Chm.Logging;
using Chm.Test.Logging.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void Test_Log_Info_Write_To_File()
        {
            string path = $"{_config.FilePath}LogFile_{DateTime.Now.ToShortDateString()}.txt";

            CleanTestFolder();

            _logger.LogInfo("Test 123");

            Assert.IsTrue(File.ReadAllText(path).StartsWith("[INFO]"), "Log message shoud start with [INFO]");
            Assert.IsTrue(File.ReadAllText(path).Contains("Test 123"));
        }

        [TestMethod]
        public void Test_Warn_Info_Write_To_File()
        {
            string path = $"{_config.FilePath}LogFile_{DateTime.Now.ToShortDateString()}.txt";

            CleanTestFolder();

            _logger.LogWarn("Test 123");

            Assert.IsTrue(File.ReadAllText(path).StartsWith("[WARN]"), "Log message shoud start with [WARN]");
            Assert.IsTrue(File.ReadAllText(path).Contains("Test 123"));
        }

        [TestMethod]
        public void Test_Error_Info_Write_To_File()
        {
            string path = $"{_config.FilePath}LogFile_{DateTime.Now.ToShortDateString()}.txt";

            CleanTestFolder();

            _logger.LogError("Test 123");

            Assert.IsTrue(File.ReadAllText(path).StartsWith("[ERROR]"), "Log message shoud start with [ERROR]");
            Assert.IsTrue(File.ReadAllText(path).Contains("Test 123"));
        }

        [TestMethod]
        public void Test_MultipleErrorLines_Write_To_File()
        {
            string path = $"{_config.FilePath}LogFile_{DateTime.Now.ToShortDateString()}.txt";

            CleanTestFolder();

            _logger.LogError("Test 1");
            _logger.LogError("Test 2");
            _logger.LogError("Test 3");

            Assert.IsTrue(File.ReadAllLines(path).Length == 3, "Should be 3 lines");
        }

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
