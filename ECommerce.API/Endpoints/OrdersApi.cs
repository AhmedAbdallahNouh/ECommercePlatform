using ECommerce.Application.Orders.Commands.CreateOrder;
using ECommerce.Application.Orders.Commands.UpdateOrder;
using ECommerce.Application.Orders.Commands.DeleteOrder;
using ECommerce.Application.Orders.Queries.GetOrderById;
using ECommerce.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Endpoints
{
    public static class OrdersApi
    {
        public static void MapOrdersEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/orders").WithTags("Orders");

            // 🔹 Get by Id
            group.MapGet("/{id:int}", async (int id, ISender sender) =>
            {
                var result = await sender.Send(new GetOrderByIdQuery(id));
                return result.IsSuccess ? Results.Ok(result.Value) : HandleFailure(result);
            }).WithName("GetOrderById");

            // 🔹 Create
            group.MapPost("/", async (CreateOrderCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);
                return result.IsSuccess
                    ? Results.Created($"/api/orders/{result.Value}", result.Value)
                    : HandleFailure(result);
            }).WithName("CreateOrder");

            // 🔹 Update
            group.MapPut("/{id:int}", async (int id, UpdateOrderCommand command, ISender sender) =>
            {
                if (id != command.Id) return Results.BadRequest("ID mismatch");
                var result = await sender.Send(command);
                return result.IsSuccess ? Results.NoContent() : HandleFailure(result);
            }).WithName("UpdateOrder");

            // 🔹 Delete
            group.MapDelete("/{id:int}", async (int id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteOrderCommand(id));
                return result.IsSuccess ? Results.NoContent() : HandleFailure(result);
            }).WithName("DeleteOrder");
        }

        private static IResult HandleFailure(Result result) =>
            Results.BadRequest(new ProblemDetails
            {
                Title = "Error",
                Detail = result.Error.Message
            });
    }
}
