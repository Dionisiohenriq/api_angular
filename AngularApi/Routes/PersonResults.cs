using AngularApi.Data;
using AngularApi.dto;
using AngularApi.Models;
using Microsoft.EntityFrameworkCore;
using Namotion.Reflection;

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

        public static async Task<IResult> GetPerson(int id, ApplicationDbContext db)
        {
            return await db.Person.FindAsync(id) is Person person
                ? TypedResults.Ok(new PersonDTO(person))
                : TypedResults.NotFound();
        }

        public static async Task<IResult> CreatePerson(PersonDTO personDTO, ApplicationDbContext db)
        {
            Person person = new() { IsSoldier = personDTO.IsSoldier, Name = personDTO.Name! };

            await db.Person.AddAsync(person);
            await db.SaveChangesAsync();

            personDTO = new PersonDTO(person);

            return TypedResults.Created($"/persons/{person.Id}", personDTO);
        }

        public static async Task<IResult> UpdatePerson(
            int id,
            PersonDTO inputPersonDTO,
            ApplicationDbContext db
        )
        {
            Person? person = await db.Person.FindAsync(id);
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
