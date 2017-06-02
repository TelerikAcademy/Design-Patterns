using Academy.Commands.Contracts;
using Academy.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Academy.Commands.Adding
{
    public class AddTrainerToSeasonCommand : ICommand
    {
        private readonly IAcademyFactory factory;
        private readonly IEngine engine;

        public AddTrainerToSeasonCommand(IAcademyFactory factory, IEngine engine)
        {
            this.factory = factory;
            this.engine = engine;
        }

        public string Execute(IList<string> parameters)
        {
            var trainerUsername = parameters[0];
            var seasonId = parameters[1];

            var trainer = this.engine.Trainers.Single(x => x.Username.ToLower() == trainerUsername.ToLower());
            var season = this.engine.Seasons[int.Parse(seasonId)];

            if (season.Trainers.Any(x => x.Username.ToLower() == trainerUsername.ToLower()))
            {
                throw new ArgumentException($"The Trainer {trainerUsername} is already a part of Season {seasonId}!");
            }

            season.Trainers.Add(trainer);
            return $"Trainer {trainerUsername} was assigned to Season {seasonId}.";
        }
    }
}
