using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Utils
{
    public class ValidatorInterceptor : IValidatorInterceptor
    {
        public ValidationResult AfterMvcValidation(ControllerContext controllerContext, IValidationContext validationContext, ValidationResult result)
        {
            if (!result.IsValid)
            {
                controllerContext.HttpContext.Items.Add("ValidationResult", result);
            }
            return result;
        }

        public IValidationContext BeforeMvcValidation(ControllerContext controllerContext, IValidationContext validationContext)
        {
            return validationContext;
        }
    }
}
