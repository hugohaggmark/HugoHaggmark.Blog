using Nancy;
using System.Collections.Generic;

namespace HugoHaggmark.Nancy.Enums.As.Strings.Modules
{
    public class Index : NancyModule
    {
        public Index()
        {
            Get["/"] = _ =>
            {
                var teamMembers = new List<TeamMember>()
                {
                    new TeamMember { Name = "Marcus", Avatar = StarWarsCharacter.Luke  },
                    new TeamMember { Name = "Hugo", Avatar = StarWarsCharacter.Chewbacca }
                };

                return Response.AsJson(teamMembers);
            };
        }
    }

    public enum StarWarsCharacter
    {
        Leia,
        Luke,
        Chewbacca
    }

    public class TeamMember
    {
        public string Name { get; set; }
        public StarWarsCharacter Avatar { get; set; }
    }
}