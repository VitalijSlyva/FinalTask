using Rental.BLL.DTO.Identity;
using Rental.BLL.DTO.Rent;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rental.BLL.Interfaces
{
    /// <summary>
    /// Interface for client actions.
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Make order.
        /// </summary>
        /// <param name="order">Order object</param>
        void MakeOrder(OrderDTO order);

        /// <summary>
        /// Get order by client id.
        /// </summary>
        /// <param name="id">Client id</param>
        /// <returns>Orders</returns>
        IEnumerable<OrderDTO> GetOrdersForClient(string id);

        /// <summary>
        /// Create payment.
        /// </summary>
        /// <param name="id">Client id</param>
        /// <param name="transactionId">Transaction id</param>
        void CreatePayment(int id,string transactionId);

        /// <summary>
        /// Create information about client.
        /// </summary>
        /// <param name="profileDTO">Profile object</param>
        void CreateProfile(ProfileDTO profileDTO);

        /// <summary>
        /// Update information about client.
        /// </summary>
        /// <param name="profileDTO">New profile object</param>
        void UpdateProfile(ProfileDTO profileDTO);

        /// <summary>
        /// Showm information by id.
        /// </summary>
        /// <param name="id">Client id</param>
        /// <returns>Profile</returns>
        Task<ProfileDTO> ShowProfileAsync(string id);

        /// <summary>
        /// Get status for order.
        /// </summary>
        /// <param name="id">Order id</param>
        /// <returns>Status</returns>
        string GetStatus(int id);

        /// <summary>
        /// Get payment by id.
        /// </summary>
        /// <param name="id">Payment id</param>
        /// <returns>Payment</returns>
        PaymentDTO GetPayment(int id);

        /// <summary>
        /// Get payments by client id.
        /// </summary>
        /// <param name="id">Client id</param>
        /// <returns>Payments</returns>
        IEnumerable<PaymentDTO> GetPaymentsForClient(string id);

        /// <summary>
        /// Client can create order.
        /// </summary>
        /// <param name="id">Client id</param>
        /// <returns>Can make order</returns>
        bool CanCreateOrder(string id);

        /// <summary>
        /// Car is free for order.
        /// </summary>
        /// <param name="carId">Car id</param>
        /// <param name="startDate">Date start order</param>
        /// <param name="endDate">Date end order</param>
        /// <returns>Is free</returns>
        bool CarIsFree(int carId, DateTime startDate, DateTime endDate);
    }
}
