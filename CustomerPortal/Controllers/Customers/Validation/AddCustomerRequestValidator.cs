using CustomerPortal.Controllers.Customers.Models;
using FluentValidation;
using System;
using System.Text.RegularExpressions;

namespace CustomerPortal.Controllers.Customers.Validation
{
    public class AddCustomerRequestValidator : AbstractValidator<AddCustomerRequest>
    {
        public AddCustomerRequestValidator()
        {
            RuleFor(x => x.FirstName).MinimumLength(3).MaximumLength(50);
            RuleFor(x => x.LastName).MinimumLength(3).MaximumLength(50);
            RuleFor(x => x.ReferenceNumber).NotEmpty().Must(HaveValidReferenceNumber);
            RuleFor(x => x.Email).NotEmpty().Must(HaveValidEmail).When(x => !string.IsNullOrWhiteSpace(x.Email));
            RuleFor(x => x.DOB).LessThanOrEqualTo(DateTime.Now.AddYears(-18)).When(x => x.DOB.HasValue);
        }

        private bool HaveValidEmail(string email)
        {
            var regex = new Regex(@"^(\w{4,})@(\w{2,})(.com|.co.uk)$");
            return regex.IsMatch(email);
        }

        private bool HaveValidReferenceNumber(string referenceNumber)
        {
            var regex = new Regex(@"^([A-Z]{2})-(\d{6})");
            return regex.IsMatch(referenceNumber);
        }
    }
}
