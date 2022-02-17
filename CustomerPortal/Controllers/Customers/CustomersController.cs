using CustomerPortal.Controllers.Customers.Models;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerPortal.Controllers.Customers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class CustomersController : ControllerBase
    {
        /// <summary>
        /// Create Customer.
        /// </summary>
        /// <param name="customer">Customer properties</param>
        [HttpPost("add")]
        public async Task<ActionResult<CustomerDetails>> CreateCustomer(AddCustomerRequest customer)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Get Customer by ID.
        /// </summary>
        /// <param name="id">ID of Customer to retrieve.</param>
        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<CustomerDetails>> GetCustomer(int id)
        {
            return new NotFoundResult();
        }
    }
}
