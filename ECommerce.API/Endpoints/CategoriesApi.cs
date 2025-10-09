using ECommerce.Application.Categories.Commands.CreateCategory;
using ECommerce.Application.Categories.Commands.UpdateCategory;
using ECommerce.Application.Categories.Commands.DeleteCategory;
using ECommerce.Application.Categories.Queries.GetAllCategories;
using ECommerce.Application.Categories.Queries.GetCategoryById;
using MediatR;
using ECommerce.API.Utilities;

namespace ECommerce.API.Endpoints
{
    public static class CategoriesApi
    {
        public static void MapCategoriesEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/categories").WithTags("Categories");

            // 🔹 Get All
            group.MapGet("/", async (ISender sender) =>
            {
                var result = await sender.Send(new GetAllCategoriesQuery());
                return result.IsSuccess ? Results.Ok(result.Value) : ApiResultHandeler.HandleFailure(result);
            }).WithName("GetAllCategories");

            // 🔹 Get By Id
            group.MapGet("/{id:int}", async (int id, ISender sender) =>
            {
                var result = await sender.Send(new GetCategoryByIdQuery(id));
                return result.IsSuccess ? Results.Ok(result.Value) : ApiResultHandeler.HandleFailure(result);
            }).WithName("GetCategoryById");

            // 🔹 Create
            group.MapPost("/", async (CreateCategoryCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);
                return result.IsSuccess
                    ? Results.Created($"/api/categories/{result.Value}", result.Value)
                    : ApiResultHandeler.HandleFailure(result);
            }).WithName("CreateCategory");

            // 🔹 Update
            group.MapPut("/{id:int}", async (int id, UpdateCategoryCommand command, ISender sender) =>
            {
                if (id != command.Id)
                    return Results.BadRequest("ID mismatch between route and body.");

                var result = await sender.Send(command);
                return result.IsSuccess ? Results.NoContent() : ApiResultHandeler.HandleFailure(result);
            }).WithName("UpdateCategory");

            // 🔹 Delete
            group.MapDelete("/{id:int}", async (int id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteCategoryCommand(id));
                return result.IsSuccess ? Results.NoContent() : ApiResultHandeler.HandleFailure(result);
            }).WithName("DeleteCategory");
        }

       
    }
}
