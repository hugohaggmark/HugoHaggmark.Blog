using Nancy;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
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
        [JsonConverter(typeof(StringEnumConverter))]
        public StarWarsCharacter Avatar { get; set; }
    }

    public class CustomSerializer : JsonSerializer
    {
        public CustomSerializer()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }

    public class BootStraper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register(typeof(JsonSerializer), typeof(CustomSerializer));
        }
    }
}