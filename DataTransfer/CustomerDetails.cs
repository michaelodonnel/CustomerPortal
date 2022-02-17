using System;

namespace DataTransfer
{
    public class CustomerDetails
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime? DOB { get; set; }
        public string Email { get; set; }
    }
}
