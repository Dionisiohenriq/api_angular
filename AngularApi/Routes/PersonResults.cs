using AngularApi.Data;
using AngularApi.DTO;
using AngularApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularApi.Routes
{
    public static class PersonResults
    {
        public static async Task<IResult> GetAllPersons(ApplicationDbContext db)
        {
            return TypedResults.Ok(await db.Person.Select(p => new PersonDTO(p)).ToArrayAsync());
        }

        public static async Task<IResult> GetSoldierPersons(ApplicationDbContext db)
        {
            return TypedResults.Ok(
                await db.Person.Where(s => s.IsSoldier).Select(p => new PersonDTO(p)).ToListAsync()
            );
        }

        public static async Task<IResult> GetPerson(Guid id, ApplicationDbContext db)
        {
            return await db.Person.FindAsync(id) is Person person
                ? TypedResults.Ok(new PersonDTO(person))
                : TypedResults.NotFound();
        }

        public static async Task<IResult> GetPersonByName(string name, ApplicationDbContext db)
        {
            return
                await db.Person.FirstOrDefaultAsync(p => p.Name.StartsWith(name)) is Person person
                ? TypedResults.Ok(new PersonDTO(person))
                : TypedResults.NotFound();
        }

        public static async Task<IResult> CreatePerson(PersonDTO personDTO, ApplicationDbContext db)
        {
            Person person =
                new()
                {                   
                    IsSoldier = personDTO.IsSoldier,
                    Name = personDTO.Name!,
                };

            await db.Person.AddAsync(person);
            await db.SaveChangesAsync();

            personDTO = new PersonDTO(person);

            return TypedResults.Created($"/persons/{person.Id}", personDTO);
        }

        public static async Task<IResult> UpdatePerson(
            string id,
            PersonDTO inputPersonDTO,
            ApplicationDbContext db
        )
        {
            Person? person = await db.Person.FindAsync(Guid.Parse(id));
            if (person is null)
                return TypedResults.NotFound();

            person.Name = inputPersonDTO.Name!;
            person.IsSoldier = inputPersonDTO.IsSoldier;

            inputPersonDTO = new PersonDTO(person);

            await db.SaveChangesAsync();

            return TypedResults.Created($"/persons/{person.Id}", inputPersonDTO);
        }

        public static async Task<IResult> DeletePerson(int id, ApplicationDbContext db)
        {
            if (await db.Person.FindAsync(id) is Person person)
            {
                db.Remove(person);
                await db.SaveChangesAsync();
                return TypedResults.NoContent();
            }

            return TypedResults.NoContent();
        }
    }
}
