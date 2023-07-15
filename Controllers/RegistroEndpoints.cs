using Microsoft.EntityFrameworkCore;
using ZooAplicationApi.Data;
using ZooAplicationApi.Data.Models;
namespace ZooAplicationApi.Controllers;

public static class RegistroEndpoints
{
    public static void MapRegistroEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Registro", async (ZoologicoApiwebDataContext db) =>
        {
            return await db.Registros.ToListAsync();
        })
        .WithName("GetAllRegistros")
        .Produces<List<Registro>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Registro/{id}", async (int Id, ZoologicoApiwebDataContext db) =>
        {
            return await db.Registros.FindAsync(Id)
                is Registro model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetRegistroById")
        .Produces<Registro>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Registro/{id}", async (int Id, Registro registro, ZoologicoApiwebDataContext db) =>
        {
            var foundModel = await db.Registros.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(registro);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateRegistro")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Registro/", async (Registro registro, ZoologicoApiwebDataContext db) =>
        {
            db.Registros.Add(registro);
            await db.SaveChangesAsync();
            return Results.Created($"/Registros/{registro.Id}", registro);
        })
        .WithName("CreateRegistro")
        .Produces<Registro>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Registro/{id}", async (int Id, ZoologicoApiwebDataContext db) =>
        {
            if (await db.Registros.FindAsync(Id) is Registro registro)
            {
                db.Registros.Remove(registro);
                await db.SaveChangesAsync();
                return Results.Ok(registro);
            }

            return Results.NotFound();
        })
        .WithName("DeleteRegistro")
        .Produces<Registro>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
