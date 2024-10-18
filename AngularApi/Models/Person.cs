namespace AngularApi.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsSoldier { get; set; }
        public string? Secret { get; set; }

        public Person(string name, bool isSoldier)
        {
            Id = Guid.NewGuid();
            Name = name;
            IsSoldier = isSoldier;
        }

        public Person() { }
    }
}
