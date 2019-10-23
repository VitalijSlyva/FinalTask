using Rental.BLL.DTO.Rent;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rental.BLL.Interfaces
{
    /// <summary>
    /// Interface for manager actions. 
    /// </summary>
    public interface IManagerService:IDisposable
    {
        /// <summary>
        /// Confirm order.
        /// </summary>
        /// <param name="confirm">Confirm object</param>
        void ConfirmOrder(ConfirmDTO confirm);

        /// <summary>
        /// Return car after driving. 
        /// </summary>
        /// <param name="returnDTO">Return object</param>
        void ReturnCar(ReturnDTO returnDTO);

        /// <summary>
        /// Get orders for return.
        /// </summary>
        /// <returns>Orders</returns>
        IEnumerable<OrderDTO> GetForReturns();

        /// <summary>
        /// Get orders for confirm.
        /// </summary>
        /// <returns>Orders.</returns>
        IEnumerable<OrderDTO> GetForConfirms();

        /// <summary>
        /// Get order by id.
        /// </summary>
        /// <param name="id">Order id</param>
        /// <param name="forConfirm">This order for confirm</param>
        /// <returns>Order</returns>
        OrderDTO GetOrder(int id,bool forConfirm);

    }
}
