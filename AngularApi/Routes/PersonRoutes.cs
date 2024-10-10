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
        persons.MapGet("/{id}", PersonResults.GetPerson);
        persons.MapPost("/", PersonResults.CreatePerson);
        persons.MapPut("/{id}", PersonResults.UpdatePerson);
        persons.MapDelete("/{id}", PersonResults.DeletePerson);
    }
}
