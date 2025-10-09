using ECommerce.API.Utilities;
using ECommerce.Application.Payments.Commands.CreatePayment;
using ECommerce.Application.Payments.Commands.DeletePayment;
using ECommerce.Application.Payments.Commands.UpdatePayment;
using ECommerce.Application.Payments.Queries.GetAllPayments;
using ECommerce.Application.Payments.Queries.GetPaymentById;
using MediatR;

namespace ECommerce.API.Endpoints
{
    public static class PaymentsApi
    {
        public static void MapPaymentsEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/payments").WithTags("Payments");

            // Get all
            group.MapGet("/", async (ISender sender) =>
            {
                var result = await sender.Send(new GetAllPaymentsQuery());
                return result.IsSuccess ? Results.Ok(result.Value) : ApiResultHandeler.HandleFailure(result);
            }).WithName("GetAllPayments");

            // Get by id
            group.MapGet("/{id:int}", async (int id, ISender sender) =>
            {
                var result = await sender.Send(new GetPaymentByIdQuery(id));
                return result.IsSuccess ? Results.Ok(result.Value) : ApiResultHandeler.HandleFailure(result);
            }).WithName("GetPaymentById");

            // Create
            group.MapPost("/", async (CreatePaymentCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);
                return result.IsSuccess
                    ? Results.Created($"/api/payments/{result.Value}", result.Value)
                    : ApiResultHandeler.HandleFailure(result);
            }).WithName("CreatePayment");

            // Update
            group.MapPut("/{id:int}", async (int id, UpdatePaymentCommand command, ISender sender) =>
            {
                if (id != command.Id) return Results.BadRequest("ID mismatch.");
                var result = await sender.Send(command);
                return result.IsSuccess ? Results.NoContent() : ApiResultHandeler.HandleFailure(result);
            }).WithName("UpdatePayment");

            // Delete
            group.MapDelete("/{id:int}", async (int id, ISender sender) =>
            {
                var result = await sender.Send(new DeletePaymentCommand(id));
                return result.IsSuccess ? Results.NoContent()  : ApiResultHandeler.HandleFailure(result);
            }).WithName("DeletePayment");
        }

        

    
    }
}
