using Microsoft.AspNetCore.Mvc;
using Simpress.Product.Domain.Entities.DTO;
using Simpress.Product.Domain.Services;

namespace Simpress.Product.API.EndPoints
{
    public static class ProductsEndpoints
    {
        public static void MapProductsEndPoints(this WebApplication app)
        {
            app.MapGet("/api/Products", async (IProductService _service) =>
            {
                return Results.Ok(await _service.GetAll());

            }).WithTags("Products");

            app.MapGet("/api/Products/{id}", async (int id,IProductService _service) =>
            {
                var result = await _service.GetById(id);

                return result.Success ? Results.Ok(result) : Results.NotFound(result);

            }).WithTags("Products");

            app.MapPut("/api/Products/{id}", async (int id,[FromBody] ProductDto productDto, IProductService _service) =>
            {
                var result = await _service.Update(productDto,id);

                return result.Success ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags("Products");

            app.MapDelete("/api/Products/{id}", async (int id, IProductService _service) =>
            {
                var result = await _service.Remove(id);

                return result.Success ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags("Products");

            app.MapPost("/api/Products", async ([FromBody] ProductDto productDto, IProductService _service) =>
            {
                var result = await _service.Add(productDto);

                return result.Success ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags("Products");
        }
    }
}
