using AngularApi.Models;

namespace AngularApi.dto
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsSoldier { get; set; }

        public PersonDTO() { }

        public PersonDTO(Person person) =>
            (Id, Name, IsSoldier) = (person.Id, person.Name, person.IsSoldier);
    }
}
