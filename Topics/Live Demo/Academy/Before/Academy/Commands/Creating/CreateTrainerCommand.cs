using Academy.Commands.Contracts;
using Academy.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Academy.Commands.Creating
{
    public class CreateTrainerCommand : ICommand
    {
        private readonly IAcademyFactory factory;
        private readonly IEngine engine;

        public CreateTrainerCommand(IAcademyFactory factory, IEngine engine)
        {
            this.factory = factory;
            this.engine = engine;
        }

        public string Execute(IList<string> parameters)
        {
            var username = parameters[0];
            var technologies = parameters[1];

            if (this.engine.Students.Any(x => x.Username.ToLower() == username.ToLower()) ||
                this.engine.Trainers.Any(x => x.Username.ToLower() == username.ToLower()))
            {
                throw new ArgumentException($"A user with the username {username} already exists!");
            }

            var trainer = this.factory.CreateTrainer(username, technologies);
            this.engine.Trainers.Add(trainer);

            return $"Trainer with ID {this.engine.Trainers.Count - 1} was created.";
        }
    }
}
