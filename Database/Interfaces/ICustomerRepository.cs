using DataTransfer;
using System.Threading.Tasks;

namespace Database.Interfaces
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Get a Customer
        /// </summary>
        /// <param name="customerId">ID of a Customer</param>
        /// <returns><see cref="CustomerDetails"/>representation of a customer</returns>
        Task<CustomerDetails> GetAsync(int customerId);

        /// <summary>
        /// Add a Customer
        /// </summary>
        /// <param name="customerId">Customer to be created</param>
        /// <returns><see cref="CustomerDetails"/>representation of a customer</returns>
        Task<CustomerDetails> AddAsync(CustomerDetails customerDetails);
    }
}
