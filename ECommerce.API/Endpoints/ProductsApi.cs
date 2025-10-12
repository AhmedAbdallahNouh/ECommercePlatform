using ECommerce.API.Utilities;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Products.Commands.CreateProductCommand;
using ECommerce.Application.Products.Commands.DeleteProductCommand;
using ECommerce.Application.Products.Commands.UpdateProductCommand;
using ECommerce.Application.Products.DTOs.ECommerce.API.Contracts.Products;
using ECommerce.Application.Products.Queries.GetAllProducts;
using ECommerce.Application.Products.Queries.GetProductById;
using ECommerce.Domain.Shared;
using ECommerce.Infrastructure.Search;
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
                    :ApiResultHandeler.HandleFailure(result);
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
                    : ApiResultHandeler.HandleFailure(result);
            }).WithName("GetProductById");


            group.MapGet("/search", async (
                [FromQuery] string term,
                [FromServices] MeiliSearchService meiliSearchService
                ) =>
            {
                if (string.IsNullOrWhiteSpace(term))
                {
                    var result = Result.Failure<int>(new Error("Product.Search ", "Search term cannot be empty."));
                    ApiResultHandeler.HandleFailure(result);
                }

                var searchResults = await meiliSearchService.SearchProductsAsync(term);
                return Results.Ok(searchResults);
            })
             .WithName("SearchProducts");


            group.MapGet("/searchpagination", async (
                [FromServices] ISearchService meiliSearchService,
                [FromQuery] string? query,
                [FromQuery] int? categoryId,
                [FromQuery] decimal? minPrice,
                [FromQuery] decimal? maxPrice,
                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize =10
                ) =>
            {
                var results = await meiliSearchService.SearchProductsAsync(query ?? "", categoryId, minPrice, maxPrice,pageNumber,pageSize);
                return Results.Ok(results);
            })
            .WithName("searchPagination")
            .WithSummary("Search products using Meilisearch")
            .WithDescription("Search products with full-text, filters, and range queries powered by Meilisearch");



            // ✅ CREATE Product (WriteDb)
            group.MapPost("/", async (CreateProductCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);
                return result.IsSuccess
                    ? Results.CreatedAtRoute("GetProductById", new { id = result.Value }, result.Value)
                    : ApiResultHandeler.HandleFailure(result);
            }).WithName("CreateProduct");

            // ✅ UPDATE Product (WriteDb)
            group.MapPut("/{id:int}", async (int id, UpdateProductCommand command, ISender sender) =>
            {
                if (id != command.Id)
                    return Results.BadRequest("Product ID mismatch");

                var result = await sender.Send(command);
                return result.IsSuccess
                    ? Results.NoContent()
                    : ApiResultHandeler.HandleFailure(result);
            }).WithName("UpdateProduct");

            // ✅ DELETE Product (WriteDb)
            group.MapDelete("/{id:int}", async (int id, ISender sender) =>
            {
                var result = await sender.Send(new DeactivateProductCommand(id));
                return result.IsSuccess
                    ? Results.NoContent()
                    : ApiResultHandeler.HandleFailure(result);
            }).WithName("DeleteProduct");
        }

       
    }
}
