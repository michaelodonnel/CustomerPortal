using AutoMapper;
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
        private readonly IMapper Mapper;

        public CustomerRepository(
            AFIContext context,
            IMapper mapper)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

            return Mapper.Map<CustomerDetails>(customer);
        }

        public async Task<CustomerDetails> GetAsync(int customerId)
        {
            var customer = await Context.Customers.FindAsync(customerId);

            if (customer == null)
            {
                throw new Exception($"No customer could not be found with ID {customerId}");
            }

            return Mapper.Map<CustomerDetails>(customer);
        }
    }
}
