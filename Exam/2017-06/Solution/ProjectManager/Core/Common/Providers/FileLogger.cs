using System;
using System.IO;

using Bytes2you.Validation;
using ProjectManager.Framework.Core.Common.Contracts;

namespace ProjectManager.Framework.Core.Common.Providers
{
    public class FileLogger : ILogger
    {
        private readonly string filePath;

        public FileLogger(string filePath)
        {
            Guard.WhenArgument(filePath, "FilePath in FileLogger").IsNullOrEmpty().Throw();

            this.filePath = filePath;
        }

        public void Info(string msg)
        {
            File.AppendAllText(this.filePath, $"INFO: {msg}{Environment.NewLine}");
        }

        public void Error(string msg)
        {
            File.AppendAllText(this.filePath, $"ERROR: {msg}{Environment.NewLine}");
        }

        public void Fatal(string msg)
        {
            File.AppendAllText(this.filePath, $"FATAL: {msg}{Environment.NewLine}");
        }
    }
}
