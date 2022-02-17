using System;

namespace CustomerPortal.Controllers.Customers.Models
{
    public class AddCustomerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime? DOB { get; set; }
        public string Email { get; set; }
    }
}
