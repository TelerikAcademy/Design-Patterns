using Academy.Commands.Contracts;
using Academy.Core.Contracts;
using System.Collections.Generic;

namespace Academy.Commands.Listing
{
    public class ListCoursesInSeasonCommand : ICommand
    {
        private readonly IAcademyFactory factory;
        private readonly IAcademyDatabase academyDatabase;

        public ListCoursesInSeasonCommand(IAcademyFactory factory, IAcademyDatabase academyDatabase)
        {
            this.factory = factory;
            this.academyDatabase= academyDatabase;
        }

        public string Execute(IList<string> parameters)
        {
            var seasonId = parameters[0];
            var season = this.academyDatabase.Seasons[int.Parse(seasonId)];

            return season.ListCourses();
        }
    }
}
