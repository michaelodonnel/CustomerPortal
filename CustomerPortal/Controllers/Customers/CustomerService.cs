using Database.Interfaces;
using DataTransfer;
using System;
using System.Threading.Tasks;

namespace CustomerPortal.Controllers.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<CustomerDetails> AddCustomerAsync(CustomerDetails customerDetails)
        {
            var createdCustomer = await _repository.AddAsync(customerDetails);
            return createdCustomer;
        }

        public async Task<CustomerDetails> GetCustomerAsync(int customerId)
        {
            return await _repository.GetAsync(customerId);
        }
    }
}
