// TODO: research how to make writing async / make class faster

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

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

        private static FileStream OutputStream;

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
            ProcessWrite(message?.ToString());
        }

        // public static void LogWarning(object message)
        // {
        //     Write(FormatLog(message?.ToString(), LogType.Warning));
        // }

        // public static void LogError(object message)
        // {
        //     Write(FormatLog(message?.ToString(), LogType.Error));
        // }

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

        private static Task ProcessWrite(string message)
        {
            return WriteTextAsync(message);
        }

        private static async Task WriteTextAsync(string message)
        {
            if (HasInitialized == false) Initialize();

            var time = DateTime.Now.ToString("h:mm:ss tt");

            byte[] encodedText = Encoding.UTF8.GetBytes($"[{time}] {message}" + "\r\n");

            using (OutputStream = new FileStream(
                OutputFilename, FileMode.Append, FileAccess.Write, FileShare.Read,
                bufferSize: 8196, useAsync: true))
            {
                await OutputStream.WriteAsync(encodedText, 0, encodedText.Length);
            }
        }



        #endregion
    }
}