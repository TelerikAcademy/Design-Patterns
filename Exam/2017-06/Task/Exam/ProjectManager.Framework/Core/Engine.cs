using Bytes2you.Validation;
using ProjectManager.Framework.Core.Commands.Factories;
using ProjectManager.Framework.Core.Common.Contracts;
using ProjectManager.Framework.Core.Common.Exceptions;
using ProjectManager.Framework.Core.Common.Providers;
using System;

namespace ProjectManager.Framework.Core
{
    public class Engine
    {
        private FileLogger logger;
        private CommandProcessor processor;

        public Engine(FileLogger logger)
        {
            this.logger = logger;
            this.processor = new CommandProcessor(new CommandsFactory());
        }

        public FileLogger Loogger
        {
            get
            {
                return this.logger;
            }

            set
            {
                Guard.WhenArgument(value, "Engine Logger provider").IsNull().Throw();
                this.logger = value;
            }
        }

        public CommandProcessor Processor
        {
            get
            {
                return this.processor;
            }

            set
            {
                Guard.WhenArgument(value, "Engine Processor provider").IsNull().Throw();
                this.processor = value;
            }
        }

        public void Start()
        {
            for (;;)
            {
                var commandLine = Console.ReadLine();

                if (commandLine.ToLower() == "exit")
                {
                    Console.WriteLine("Program terminated.");
                    break;
                }

                try
                {
                    var executionResult = this.processor.ProcessCommand(commandLine);
                    Console.WriteLine(executionResult);
                }
                catch (UserValidationException ex)
                {
                    this.logger.Error(ex.Message);
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Opps, something happened. Check the log file :(");
                    this.logger.Error(ex.Message);
                }
            }
        }
    }
}
