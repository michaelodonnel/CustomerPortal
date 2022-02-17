using CustomerPortal.Controllers.Customers.Models;
using FluentValidation;
using System;

namespace CustomerPortal.Controllers.Customers.Validation
{
    public class AddCustomerRequestValidator : AbstractValidator<AddCustomerRequest>
    {
        public AddCustomerRequestValidator()
        {
            RuleFor(x => x.FirstName).MinimumLength(3).MaximumLength(50);
            RuleFor(x => x.LastName).MinimumLength(3).MaximumLength(50);
            // TODO implement validation logic for reference number and email using regex
            RuleFor(x => x.DOB).LessThanOrEqualTo(DateTime.Now.AddYears(-18)).When(x => x.DOB.HasValue);
        }

    }
}
