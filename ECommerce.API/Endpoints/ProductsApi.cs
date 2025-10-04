using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Products.Commands.CreateProductCommand;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Endpoints
{
    public static class ProductsApi
    {
        public static async Task MapRoductsEndPointsAsync(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/products").WithTags("Products");

            group.MapGet("/", async (IUnitOfWork unitOfWork) =>
            {
                var products = await unitOfWork.ReadRepository<Product>().GetAllAsync();
                return Results.Ok(products);
            }).WithName("GetAllProducts");


            group.MapGet("/{id:int}", async (int id, IUnitOfWork unitOfWork) =>
            {
                var product = await unitOfWork.ReadRepository<Product>().GetByIdAsync(id);
                return product is not null ? Results.Ok(product) : Results.NotFound();
            }).WithName("GetProductById");

            group.MapPost("/", async (CreateProductCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);
                return result.IsSuccess ? Results.CreatedAtRoute("GetProductById", new { id = result.Value }, result.Value) 
                : HandleFailure(result);
            }).WithName("CreateProduct");

           group.MapPut("/{id:int}", async (int id, Product updatedProduct, IUnitOfWork unitOfWork) =>
            {
                var product = await unitOfWork.ReadRepository<Product>().GetByIdAsync(id);
                if (product is null) return Results.NotFound();
                product.Name = updatedProduct.Name;
                product.Description = updatedProduct.Description;
                product.Price = updatedProduct.Price;
                unitOfWork.WriteRepository<Product>().Update(product);
                await unitOfWork.SaveChangesAsync();
                return Results.NoContent();
            }).WithName("UpdateProduct");

            group.MapDelete("/{id:int}", async (int id, IUnitOfWork unitOfWork) =>
            {
                var product = await unitOfWork.ReadRepository<Product>().GetByIdAsync(id);
                if (product is null) return Results.NotFound();
                unitOfWork.WriteRepository<Product>().Delete(product);
                await unitOfWork.SaveChangesAsync();
                return Results.NoContent();
            }).WithName("DeleteProduct");
        }
    
        public static IResult HandleFailure(Result result) => 
            result switch
            {
                { IsSuccess : true } => throw new InvalidOperationException(),
                IValidationResult validationResult => 
                     Results.BadRequest(
                        CreateProblemDetails(
                            "Validation Error", StatusCodes.Status400BadRequest,
                            result.Error, 
                            validationResult.Errors)),

                _ => Results.BadRequest(
                        CreateProblemDetails(
                            "Bad Request", StatusCodes.Status400BadRequest,
                            result.Error))
            };
      
        private static ProblemDetails CreateProblemDetails(string title, int status, Error error, Error[]? errors = null ) =>
            new()
            {
                Title = title,
                Type = error.Code,
                Detail = error.Message,
                Status = status,
                Extensions =
                {
                    ["errors"] = errors?.Select(e => new { e.Code, e.Message })
                }
            };
    }

}
