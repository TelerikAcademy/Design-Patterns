using Academy.Commands.Contracts;
using Academy.Core.Contracts;
using System.Collections.Generic;

namespace Academy.Commands.Creating
{
    public class CreateSeasonCommand : ICommand
    {
        private readonly IAcademyFactory factory;
        private readonly IAcademyDatabase academyDatabase;

        public CreateSeasonCommand(IAcademyFactory factory, IAcademyDatabase academyDatabase)
        {
            this.factory = factory;
            this.academyDatabase= academyDatabase;
        }

        public string Execute(IList<string> parameters)
        {
            var startingYear = parameters[0];
            var endingYear = parameters[1];
            var initiative = parameters[2];

            var season = this.factory.CreateSeason(startingYear, endingYear, initiative);
            this.academyDatabase.Seasons.Add(season);

            return $"Season with ID {this.academyDatabase.Seasons.Count - 1} was created.";
        }
    }
}
