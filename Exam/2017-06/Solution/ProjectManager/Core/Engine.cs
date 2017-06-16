using Bytes2you.Validation;
using ProjectManager.Framework.Core.Common.Contracts;
using System;

namespace ProjectManager.Framework.Core
{
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;
        private IProcessor processor;

        public Engine(IReader reader, IWriter writer, IProcessor processor)
        {
            if(processor == null)
            {
                throw new ArgumentNullException();
            }

            Guard.WhenArgument(reader, "reader").IsNull().Throw();
            Guard.WhenArgument(writer, "writer").IsNull().Throw();

            this.reader = reader;
            this.writer = writer;
            this.processor = processor;
        }

        public void Start()
        {
            for (;;)
            {
                var commandLine = this.reader.ReadLine();

                if (commandLine.ToLower() == "exit")
                {
                    this.writer.WriteLine("Program terminated.");
                    break;
                }

                this.processor.ProcessCommand(commandLine);
            }
        }
    }
}
