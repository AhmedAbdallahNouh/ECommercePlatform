using ECommerce.API.Utilities;
using ECommerce.Application.Notifications.Commands.CreateNotification;
using ECommerce.Application.Notifications.Commands.DeleteNotification;
using ECommerce.Application.Notifications.Commands.UpdateNotification;
using ECommerce.Application.Notifications.Queries.GetAllNotifications;
using ECommerce.Application.Notifications.Queries.GetNotificationById;
using ECommerce.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Endpoints
{
    public static class NotificationsApi
    {
        public static void MapNotificationsEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/notifications").WithTags("Notifications");

            group.MapGet("/", async (ISender sender) =>
            {
                var result = await sender.Send(new GetAllNotificationsQuery());
                return result.IsSuccess ? Results.Ok(result.Value) : ApiResultHandeler.HandleFailure(result);
            }).WithName("GetAllNotifications");

            group.MapGet("/{id:int}", async (int id, ISender sender) =>
            {
                var result = await sender.Send(new GetNotificationByIdQuery(id));
                return result.IsSuccess ? Results.Ok(result.Value) : ApiResultHandeler.HandleFailure(result);
            }).WithName("GetNotificationById");

            group.MapPost("/", async (CreateNotificationCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);
                return result.IsSuccess
                    ? Results.Created($"/api/notifications/{result.Value}", result.Value)
                    : ApiResultHandeler.HandleFailure(result);
            }).WithName("CreateNotification");

            group.MapPut("/{id:int}", async (int id, UpdateNotificationCommand command, ISender sender) =>
            {
                if (id != command.Id) return Results.BadRequest("ID mismatch.");
                var result = await sender.Send(command);
                return result.IsSuccess ? Results.NoContent() : ApiResultHandeler.HandleFailure(result);
            }).WithName("UpdateNotification");

            group.MapDelete("/{id:int}", async (int id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteNotificationCommand(id));
                return result.IsSuccess ? Results.NoContent() : ApiResultHandeler.HandleFailure(result);
            }).WithName("DeleteNotification");
        }
    }
}
