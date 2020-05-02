// TODO: research how to make writing async

using System;
using System.IO;

namespace Console3D
{
    public static class Debug
    {
        public enum LogType
        {
            Info,
            Warning,
            Error
        }

        #region Properties

        private static bool HasInitialized;

        private static string OutputFilename;

        private static StreamWriter OutputWriter;

        #endregion

        #region Public Methods

        private static void Initialize()
        {
            OutputFilename = Path.Combine(
                $@"{System.AppDomain.CurrentDomain.BaseDirectory}",
                "debug.txt"
            );

            if (File.Exists(OutputFilename))
            {
                File.WriteAllText(OutputFilename, String.Empty);
            }
            else
            {
                File.Create(OutputFilename);
            }

            HasInitialized = true;
        }

        public static void Log(object message)
        {
            Write(message?.ToString());
        }

        public static void LogWarning(object message)
        {
            Write(FormatLog(message?.ToString(), LogType.Warning));
        }

        public static void LogError(object message)
        {
            Write(FormatLog(message?.ToString(), LogType.Error));
        }

        #endregion

        #region Private Methods

        private static string FormatLog(string message, LogType logType)
        {
            var time = DateTime.Now.ToString("h:mm:ss tt");
            string prefix = null;

            switch (logType)
            {
                default:
                case LogType.Info:
                    break;
                case LogType.Warning:
                    prefix = "[WARNING] ";
                    break;
                case LogType.Error:
                    prefix = "[ERROR] ";
                    break;
            }

            return $"[{time}] {prefix}{message}";
        }

        private static async void Write(string message)
        {
            if (HasInitialized == false) Initialize();

            var time = DateTime.Now.ToString("h:mm:ss tt");

            using (OutputWriter = new StreamWriter(OutputFilename, true))
            {
                await OutputWriter.WriteLineAsync($"[{time}] {message}");
            }
        }

        #endregion
    }
}