using Microsoft.AspNetCore.Mvc;
using Tools.Result;

namespace Identity.Service.API.Extensions
{
    public static class CqsResultExtension
    {
        public static IActionResult ToActionResult(this CqsResult result) 
        {
            if (result.IsSuccess)
            {
                return result.StatusCode switch
                {

                    201 => new StatusCodeResult(StatusCodes.Status201Created),
                    204 => new StatusCodeResult(StatusCodes.Status204NoContent),
                    _ => new OkResult()
                };
            }

            return result.Error.Code switch
            {
                "VALIDATION_ERROR" => new BadRequestObjectResult(result),
                "NOT_FOUND" => new NotFoundObjectResult(result),
                _ => new ObjectResult(result)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                }
            };
        }
    }
}
