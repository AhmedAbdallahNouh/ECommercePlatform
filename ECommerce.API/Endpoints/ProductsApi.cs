using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Products.Commands.CreateProductCommand;
using ECommerce.Domain.Models;
using MediatR;

namespace ECommerce.API.Endpoints
{
    public static class ProductsApi
    {
        public static async Task MapRoductsEndPointsAsync(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/products").WithTags("Products");

            group.MapGet("/", async (IUnitOfWork unitOfWork) =>
            {
                var products = await unitOfWork.Repository<Product>().GetAllAsync();
                return Results.Ok(products);
            }).WithName("GetAllProducts");


            group.MapGet("/{id:int}", async (int id, IUnitOfWork unitOfWork) =>
            {
                var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);
                return product is not null ? Results.Ok(product) : Results.NotFound();
            }).WithName("GetProductById");

            group.MapPost("/", async (CreateProductCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);
                return result.IsSuccess ? Results.CreatedAtRoute("GetProductById", new { id = result.Value }, result.Value) 
                : Results.BadRequest(result.Error);
            }).WithName("CreateProduct");

           group.MapPut("/{id:int}", async (int id, Product updatedProduct, IUnitOfWork unitOfWork) =>
            {
                var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);
                if (product is null) return Results.NotFound();
                product.Name = updatedProduct.Name;
                product.Description = updatedProduct.Description;
                product.Price = updatedProduct.Price;
                unitOfWork.Repository<Product>().Update(product);
                await unitOfWork.SaveChangesAsync();
                return Results.NoContent();
            }).WithName("UpdateProduct");

            group.MapDelete("/{id:int}", async (int id, IUnitOfWork unitOfWork) =>
            {
                var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);
                if (product is null) return Results.NotFound();
                unitOfWork.Repository<Product>().Delete(product);
                await unitOfWork.SaveChangesAsync();
                return Results.NoContent();
            }).WithName("DeleteProduct");
        }
    }
}
