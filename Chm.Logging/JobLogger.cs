using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Chm.Logging
{
    public class JobLogger : IJobLogger
    {
        protected readonly object LockObj = new object();

        private readonly IJobLoggerConfig _config;

        public JobLogger(IJobLoggerConfig config)
        {
            _config = config;
        }

        public void LogInfo(string message)
        {
            this.Log(message, LogLevel.Info);
        }

        public void LogWarn(string message)
        {
            this.Log(message, LogLevel.Warn);
        }

        public void LogError(string message)
        {
            this.Log(message, LogLevel.Err);
        }

        private void Log(string message, LogLevel level)
        {
            if ((int) level < _config.LogSeverity)
            {
                return;
            }

            message = PrepareMessage(message, level);

            if (_config.IsLogToConsole)
            {
                this.WriteToConsole(message, level);
            }

            if (_config.IsLogToFile)
            {
                this.WriteToFile(message, level);
            }

            if (_config.IsLogToDataBase)
            {
                this.WriteToDb(message, level);
            }
        }

        private string PrepareMessage(string message, LogLevel level)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            message = $"{DateTime.Now} {message.Trim()}";

            switch (level)
            {
                case LogLevel.Warn:
                    message = $"[WARN] {message}";
                    break;
                case LogLevel.Err:
                    message = $"[ERRO] {message}";
                    break;
                default:
                    message = $"[INFO] {message}";
                    break;
            }

            return message;
        }

        private void WriteToDb(string message, LogLevel level)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config.DbConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Insert into Log Values('" + message + "', " + (int)level + ")");
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception x)
            {
                throw new ApplicationException("Error during writing log to database", x);
            }
        }

        private void WriteToFile(string message, LogLevel level)
        {
            var filePath = $"{_config.FilePath}LogFile_{DateTime.Now.ToShortDateString()}.txt";
            try
            {
                lock (LockObj)
                {
                    using (StreamWriter streamWriter = new StreamWriter(filePath, true))
                    {
                        streamWriter.WriteLine(message);
                        streamWriter.Close();
                    }
                }
            }
            catch (Exception x)
            {
                throw new ApplicationException("Error during writing log to file", x);
            }
        }

        private void WriteToConsole(string message, LogLevel level)
        {
            try
            {
                switch (level)
                {
                    case LogLevel.Err:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case LogLevel.Warn:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
                Console.WriteLine(message);
            }
            catch (Exception x)
            {
                throw new ApplicationException("Error during writing log to console", x);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
