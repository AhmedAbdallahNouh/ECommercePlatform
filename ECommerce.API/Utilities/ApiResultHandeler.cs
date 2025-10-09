using ECommerce.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Utilities
{
    public static class ApiResultHandeler
    {
        public static IResult HandleFailure(Result result) =>
            Results.BadRequest(new ProblemDetails
            {
                Title = "Error",
                Detail = result.Error.Message
            });
    }
}
