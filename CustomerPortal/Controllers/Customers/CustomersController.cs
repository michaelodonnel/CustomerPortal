using CustomerPortal.Controllers.Customers.Models;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CustomerPortal.Controllers.Customers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        /// <summary>
        /// Create Customer.
        /// </summary>
        /// <param name="customer">Customer properties</param>
        [HttpPost("add")]
        public async Task<ActionResult<CustomerDetails>> CreateCustomer(AddCustomerRequest customer)
        {
            var customerToCreate = new CustomerDetails()
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                ReferenceNumber = customer.ReferenceNumber,
                Email = customer.Email,
                DOB = customer.DOB?.Date
            };

            var createdCustomer = await _customerService.AddCustomerAsync(customerToCreate);
            return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomer.Id }, createdCustomer);
        }

        /// <summary>
        /// Get Customer by ID.
        /// </summary>
        /// <param name="id">ID of Customer to retrieve.</param>
        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<CustomerDetails>> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomerAsync(id);
            return Ok(customer);
        }
    }
}
