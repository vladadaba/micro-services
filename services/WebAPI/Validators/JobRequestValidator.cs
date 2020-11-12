using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using WebAPI.DTO;

namespace WebAPI.Validators
{
	public class JobRequestValidator : AbstractValidator<JobRequest>
	{
		public JobRequestValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithErrorCode("NOT_EMPTY")
				.MaximumLength(50).WithErrorCode("MAX_LENGTH");
		}
	}
}
