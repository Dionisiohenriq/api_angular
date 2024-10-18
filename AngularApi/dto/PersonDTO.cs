using AngularApi.Models;
using Newtonsoft.Json;

namespace AngularApi.DTO
{
    public class PersonDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("isSoldier")]
        public bool IsSoldier { get; set; }

        public PersonDTO() { }

        public PersonDTO(Person person) =>
            (Id, Name, IsSoldier) = (person.Id.ToString(), person.Name, person.IsSoldier);
    }
}
