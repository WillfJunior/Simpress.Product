using Microsoft.AspNetCore.Mvc;
using Simpress.Product.Domain.Entities.DTO;
using Simpress.Product.Domain.Services;

namespace Simpress.Product.API.EndPoints
{
    public static class CategoryEndpoints
    {
        public static void MapCategorysEndPoints(this WebApplication app)
        {
            app.MapGet("/api/Categories", async (ICategoryService _service) =>
            {
                return Results.Ok(await _service.GetAll());

            }).WithTags("Categories");

            app.MapGet("/api/Categories/{id}", async (int id, ICategoryService _service) =>
            {
                var result = await _service.GetById(id);

                return result.Success ? Results.Ok(result) : Results.NotFound(result);

            }).WithTags("Categories");

            app.MapPut("/api/Categories/{id}", async (int id, [FromBody] CategoryDto categoryDto, ICategoryService _service) =>
            {
                var result = await _service.Update(categoryDto, id);

                return result.Success ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags("Categories");

            app.MapDelete("/api/Categories/{id}", async (int id, ICategoryService _service) =>
            {
                var result = await _service.Remove(id);

                return result.Success ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags("Categories");

            app.MapPost("/api/Categories", async ([FromBody] CategoryDto CategoryDto, ICategoryService _service) =>
            {
                var result = await _service.Add(CategoryDto);

                return result.Success ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags("Categories");
        }
    }
}
