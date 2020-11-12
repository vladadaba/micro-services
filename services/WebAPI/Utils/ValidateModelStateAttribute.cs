using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.Utils
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            if (!ctx.HttpContext.Items.TryGetValue("ValidationResult", out var value))
            {
                return;
            }
            
            if (!(value is ValidationResult vldResult))
            {
                return;
            }

            var validationErrors = vldResult.Errors
                .GroupBy(err => err.PropertyName)
                .ToDictionary(group => group.Key, group => group.Select(
                    err => new
                    {
                        err.ErrorMessage,
                        err.ErrorCode
                    }));

            ctx.Result = new BadRequestObjectResult(new {
                validationErrors
            });
        }
    }
}
