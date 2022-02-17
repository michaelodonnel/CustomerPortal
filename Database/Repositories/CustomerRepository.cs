using Database.Entities;
using Database.Interfaces;
using DataTransfer;
using System;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AFIContext Context;

        public CustomerRepository(AFIContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<CustomerDetails> AddAsync(CustomerDetails customerDetails)
        {
            var customer = new Customer
            {
                FirstName = customerDetails.FirstName,
                LastName = customerDetails.LastName,
                ReferenceNumber = customerDetails.ReferenceNumber,
                DOB = customerDetails.DOB,
                Email = customerDetails.Email
            };

            Context.Customers.Add(customer);
            await Context.SaveChangesAsync();

            customerDetails.Id = customer.Id;

            return customerDetails;
        }

        public async Task<CustomerDetails> GetAsync(int customerId)
        {
            var customer = await Context.Customers.FindAsync(customerId);

            if (customer == null)
            {
                throw new Exception($"No customer could not be found with ID {customerId}");
            }

            return new CustomerDetails
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                ReferenceNumber = customer.ReferenceNumber,
                DOB = customer.DOB,
                Email = customer.Email
            };
        }
    }
}
