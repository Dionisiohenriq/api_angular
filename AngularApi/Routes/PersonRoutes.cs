using AngularApi.Data;
using AngularApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularApi.Routes;

public static class PersonRoutes
{
    public static void MapPersonRoutes(this WebApplication app)
    {
        var persons = app.MapGroup("/persons");

        persons.MapGet("/", PersonResults.GetAllPersons);
        persons.MapGet("/soldiers", PersonResults.GetSoldierPersons);
        persons.MapGet("/getById/{id:guid}", PersonResults.GetPerson);
        persons.MapGet("/getByName/{name}", PersonResults.GetPersonByName);
        persons.MapPost("/", PersonResults.CreatePerson);
        persons.MapPut("/{id:guid}", PersonResults.UpdatePerson);
        persons.MapDelete("/{id:guid}", PersonResults.DeletePerson);
    }
}
