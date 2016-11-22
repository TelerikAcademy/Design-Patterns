using Dealership.CommandHandlers;
using Dealership.Common;
using Dealership.Common.Enums;
using Dealership.Contracts;
using Dealership.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Engine
{
    public sealed class DealershipEngine : IEngine
    {
        private readonly IDealershipFactory dealershipFactory;
        private readonly ICommandHandlerProcessor commandHandler;
        private readonly IInputOutputProvider inputOutputProvider;

        public DealershipEngine(IDealershipFactory dealershipFactory, ICommandHandlerProcessor commandHandler, IInputOutputProvider inputOutputProvider)
        {
            this.dealershipFactory = dealershipFactory;
            this.commandHandler = commandHandler;
            this.inputOutputProvider = inputOutputProvider;
        }

        public void Start()
        {
            var commands = this.ReadCommands();
            var commandResult = this.ProcessCommands(commands);
            this.PrintReports(commandResult);
        }
        

        private IEnumerable<ICommand> ReadCommands()
        {
            var commands = new List<ICommand>();

            var currentLine = this.inputOutputProvider.Read();

            while (!string.IsNullOrEmpty(currentLine))
            {
                var currentCommand = this.dealershipFactory.CreateCommand(currentLine);
                commands.Add(currentCommand);

                currentLine = this.inputOutputProvider.Read();
            }

            return commands;
        }

        private IEnumerable<string> ProcessCommands(IEnumerable<ICommand> commands)
        {
            var reports = new List<string>();

            foreach (var command in commands)
            {
                try
                {
                    var report = this.commandHandler.ProcessCommand(command);
                    reports.Add(report);
                }
                catch (Exception ex)
                {
                    reports.Add(ex.Message);
                }
            }

            return reports;
        }

        private void PrintReports(IEnumerable<string> reports)
        {
            var output = new StringBuilder();

            foreach (var report in reports)
            {
                output.AppendLine(report);
                output.AppendLine(new string('#', 20));
            }

            this.inputOutputProvider.Write(output.ToString());
        }
    }
}
