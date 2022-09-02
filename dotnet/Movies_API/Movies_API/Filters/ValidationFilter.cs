using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Movies.Services.Models.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Api.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        /// <inheritdoc />
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

                var errors = new List<string>();

                foreach (var (key, value) in errorsInModelState)
                {
                    errors.AddRange(value.Select(subError => $"{key} : {subError}"));
                }

                context.Result = new BadRequestObjectResult(new ApiResponse<dynamic>
                {
                    Success = false,
                    Errors = errors,
                    Messages = new List<string> { "Model is not valid" }
                });

                return;
            }

            await next();
        }
    }
}


