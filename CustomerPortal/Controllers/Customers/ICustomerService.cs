using DataTransfer;
using System.Threading.Tasks;

namespace CustomerPortal.Controllers.Customers
{
    public interface ICustomerService
    {
        /// <summary>
        /// Get a Customer
        /// </summary>
        /// <param name="customerId">ID of a Customer</param>
        /// <returns><see cref="CustomerDetails"/>representation of a customer</returns>
        Task<CustomerDetails> GetCustomerAsync(int customerId);

        /// <summary>
        /// Add Customer
        /// </summary>
        /// <param name="CustomerDetails">Customer to be created</param>
        /// <returns><see cref="CustomerDetails"/>representation of added customer</returns>
        Task<CustomerDetails> AddCustomerAsync(CustomerDetails customerDetails);
    }
}
