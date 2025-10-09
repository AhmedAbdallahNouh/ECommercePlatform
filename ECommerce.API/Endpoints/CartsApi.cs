using ECommerce.API.Utilities;
using ECommerce.Application.Carts.Commands.CreateCart;
using ECommerce.Application.Carts.Commands.DeleteCart;
using ECommerce.Application.Carts.Commands.UpdateCart;
using ECommerce.Application.Carts.Queries.GetAllCarts;
using ECommerce.Application.Carts.Queries.GetCartById;
using MediatR;

namespace ECommerce.API.Endpoints
{
    public static class CartsApi
    {
        public static void MapCartsEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/carts").WithTags("Carts");

            group.MapGet("/", async (ISender sender) =>
            {
                var result = await sender.Send(new GetAllCartsQuery());
                return result.IsSuccess ? Results.Ok(result.Value) : ApiResultHandeler.HandleFailure(result);
            }).WithName("GetAllCarts");

            group.MapGet("/{id:int}", async (int id, ISender sender) =>
            {
                var result = await sender.Send(new GetCartByIdQuery(id));
                return result.IsSuccess ? Results.Ok(result.Value) : ApiResultHandeler.HandleFailure(result);
            }).WithName("GetCartById");

            group.MapPost("/", async (CreateCartCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);
                return result.IsSuccess
                    ? Results.Created($"/api/carts/{result.Value}", result.Value)
                    : ApiResultHandeler.HandleFailure(result);
            }).WithName("CreateCart");

            group.MapPut("/{id:int}", async (int id, UpdateCartCommand command, ISender sender) =>
            {
                if (id != command.Id) return Results.BadRequest("ID mismatch.");
                var result = await sender.Send(command);
                return result.IsSuccess ? Results.NoContent() : ApiResultHandeler.HandleFailure(result);
            }).WithName("UpdateCart");

            group.MapDelete("/{id:int}", async (int id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteCartCommand(id));
                return result.IsSuccess ? Results.NoContent() : ApiResultHandeler.HandleFailure(result);
            }).WithName("DeleteCart");
        }

       
    }
}
