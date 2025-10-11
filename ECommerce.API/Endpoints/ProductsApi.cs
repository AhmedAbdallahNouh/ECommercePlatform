using ECommerce.Application.Products.Commands.CreateProductCommand;
using ECommerce.Application.Products.Commands.DeleteProductCommand;
using ECommerce.Application.Products.Commands.UpdateProductCommand;
using ECommerce.Application.Products.DTOs.ECommerce.API.Contracts.Products;
using ECommerce.Application.Products.Queries.GetAllProducts;
using ECommerce.Application.Products.Queries.GetProductById;
using ECommerce.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Endpoints
{
    public static class ProductsApi
    {
        public static void MapProductsEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/products").WithTags("Products");

            // ✅ GET All Products (ReadDb)
            // ✅ GET All Products (with filtering, searching, pagination)
            group.MapGet("/", async (
                [AsParameters] GetAllProductsRequest request,
                ISender sender) =>
            {
                var query = new GetAllProductsQuery(
                    request.SearchTerm,
                    request.CategoryId,
                    request.PageNumber,
                    request.PageSize
                );

                var result = await sender.Send(query);

                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : HandleFailure(result);
            })
            .WithName("GetAllProducts")
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Get all products with optional search, category filter, and pagination."
            });

            // ✅ GET Product by Id (ReadDb)
            group.MapGet("/{id:int}", async (int id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQeury(id));
                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : HandleFailure(result);
            }).WithName("GetProductById");

            // ✅ CREATE Product (WriteDb)
            group.MapPost("/", async (CreateProductCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);
                return result.IsSuccess
                    ? Results.CreatedAtRoute("GetProductById", new { id = result.Value }, result.Value)
                    : HandleFailure(result);
            }).WithName("CreateProduct");

            // ✅ UPDATE Product (WriteDb)
            group.MapPut("/{id:int}", async (int id, UpdateProductCommand command, ISender sender) =>
            {
                if (id != command.Id)
                    return Results.BadRequest("Product ID mismatch");

                var result = await sender.Send(command);
                return result.IsSuccess
                    ? Results.NoContent()
                    : HandleFailure(result);
            }).WithName("UpdateProduct");

            // ✅ DELETE Product (WriteDb)
            group.MapDelete("/{id:int}", async (int id, ISender sender) =>
            {
                var result = await sender.Send(new DeactivateProductCommand(id));
                return result.IsSuccess
                    ? Results.NoContent()
                    : HandleFailure(result);
            }).WithName("DeleteProduct");
        }

        // ✅ Unified Error Handling
        private static IResult HandleFailure(Result result) =>
            result switch
            {
                { IsSuccess: true } => throw new InvalidOperationException(),
                IValidationResult validationResult =>
                    Results.BadRequest(CreateProblemDetails(
                        "Validation Error",
                        StatusCodes.Status400BadRequest,
                        result.Error,
                        validationResult.Errors)),

                _ => Results.BadRequest(CreateProblemDetails(
                        "Bad Request",
                        StatusCodes.Status400BadRequest,
                        result.Error))
            };

        private static ProblemDetails CreateProblemDetails(string title, int status, Error error, Error[]? errors = null) =>
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
