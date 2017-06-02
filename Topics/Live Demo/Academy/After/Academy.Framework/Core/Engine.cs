using Academy.Core.Contracts;
using Academy.Core.Factories;
using System;
using System.Text;

namespace Academy.Core
{
    public class Engine : IEngine
    {
        private const string TerminationCommand = "Exit";
        private const string NullProvidersExceptionMessage = "cannot be null.";
        private readonly StringBuilder builder = new StringBuilder();

        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IParser parser;
        private readonly ICommandFactory commandFactory;

        public Engine(IReader reader, IWriter writer, IParser parser, ICommandFactory commandFactory)
        {
            this.reader = reader;
            this.writer = writer;
            this.parser = parser;
            this.commandFactory = commandFactory;
        }

        public void Start()
        {
            while (true)
            {
                try
                {
                    var commandAsString = this.reader.ReadLine();

                    if (commandAsString == TerminationCommand)
                    {
                        this.writer.Write(this.builder.ToString());
                        break;
                    }

                    this.ProcessCommand(commandAsString);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    this.builder.AppendLine("Invalid command parameters supplied or the entity with that ID for does not exist.");
                }
                catch (Exception ex)
                {
                    this.builder.AppendLine(ex.Message);
                }
            }
        }

        private void ProcessCommand(string commandAsString)
        {
            if (string.IsNullOrWhiteSpace(commandAsString))
            {
                throw new ArgumentNullException("Command cannot be null or empty.");
            }

            var command = this.commandFactory.GetCommand(commandAsString);
            var parameters = this.parser.ParseParameters(commandAsString);
            var executionResult = command.Execute(parameters);

            this.builder.AppendLine(executionResult);
        }
    }
}
