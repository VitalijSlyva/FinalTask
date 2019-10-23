using Microsoft.AspNet.Identity;
using Rental.BLL.Abstracts;
using Rental.BLL.DTO.Identity;
using Rental.BLL.DTO.Rent;
using Rental.BLL.Interfaces;
using Rental.DAL.Entities.Identity;
using Rental.DAL.Entities.Rent;
using Rental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rental.BLL.Services
{
    /// <summary>
    /// Service for manager actions.
    /// </summary>
    public class ManagerService :Service, IManagerService
    {
        /// <summary>
        /// Create units and mappers for work.
        /// </summary>
        /// <param name="mapperDTO">Mapper for converting database entities to DTO entities</param>
        /// <param name="rentUnit">Rent unit of work</param>
        /// <param name="identityUnit">Udentity unit of work</param>
        /// <param name="identityMapper">Mapper for converting identity entities to BLL classes</param>
        /// <param name="log">Service for logging</param>
        public ManagerService(IRentMapperDTO mapperDTO, IRentUnitOfWork rentUnit,
                IIdentityUnitOfWork identityUnit, IIdentityMapperDTO identityMapper,ILogService log)
                  : base(mapperDTO, rentUnit, identityUnit, identityMapper,log)
        {
            
        }

        /// <summary>
        /// Get orders for confirm.
        /// </summary>
        /// <returns>Orders.</returns>
        public IEnumerable<OrderDTO> GetForConfirms()
        {
            try
            {
                var orders = RentUnitOfWork.Orders.Show()
                    .Where(x => (x.Confirm == null||x.Confirm.Count==0)&&x.Payment.First().IsPaid);

                return RentMapperDTO.ToOrderDTO.Map<IEnumerable<Order>, List<OrderDTO>>(orders);
            }
            catch (Exception e)
            {
                CreateLog(e, "ManagerService", "GetForConfirms");

                return null;
            }
        }

        /// <summary>
        /// Get orders for return.
        /// </summary>
        /// <returns>Orders</returns>
        public IEnumerable<OrderDTO> GetForReturns()
        {
            try
            {
                var orders = RentUnitOfWork.Orders.Show()
                    .Where(x => x.Confirm != null&&x.Confirm.Count>0 &&( x.Return == null||x.Return.Count==0 )&&
                    x.Confirm.First().IsConfirmed);

                return RentMapperDTO.ToOrderDTO.Map<IEnumerable<Order>, List<OrderDTO>>(orders);
            }
            catch (Exception e)
            {
                CreateLog(e, "ManagerService", "GetForReturns");

                return null;
            }
        }

        /// <summary>
        /// Get order by id.
        /// </summary>
        /// <param name="id">Order id</param>
        /// <param name="forConfirm">This order for confirm</param>
        /// <returns>Order</returns>
        public OrderDTO GetOrder(int id,bool forConfirm)
        {
            try
            {
                var order = RentUnitOfWork.Orders.Get(id);
                if (order == null)
                    return null;
                if (forConfirm && order.Confirm != null&&order.Confirm.Count>0)
                    return null;
                if (!forConfirm && ((order.Return != null &&order.Return.Count>0 ) ||
                    order.Confirm == null||order.Confirm.Count==0 || !order.Confirm.First().IsConfirmed))
                    return null;
                var orderDTO = RentMapperDTO.ToOrderDTO.Map<Order, OrderDTO>(order);
                orderDTO.Profile =IdentityMapperDTO.ToProfileDTO
                    .Map<Profile,ProfileDTO>(IdentityUnitOfWork.UserManager.FindById(order.ClientId).Profile);

                return orderDTO;
            }
            catch (Exception e)
            {
                CreateLog(e, "ManagerService", "GetOrder");

                return null;
            }
        }

        /// <summary>
        /// Confirm order.
        /// </summary>
        /// <param name="confirmDTO">Confirm object</param>
        public void ConfirmOrder(ConfirmDTO confirmDTO)
        {
            try
            {
                if (RentUnitOfWork.Orders.Get(confirmDTO.Order.Id).Confirm == null||
                    RentUnitOfWork.Orders.Get(confirmDTO.Order.Id).Confirm.Count==0)
                {
                    var confirm = RentMapperDTO.ToConfirm.Map<ConfirmDTO, Confirm>(confirmDTO);
                    confirm.ManagerId = confirmDTO.User.Id;
                    confirm.Order = RentUnitOfWork.Orders.Get(confirmDTO.Order.Id);
                    RentUnitOfWork.Confirms.Create(confirm);
                    RentUnitOfWork.Save();
                }
            }
            catch (Exception e)
            {
                CreateLog(e, "ManagerService", "ConfirmOrder");
            }
        }

        /// <summary>
        /// Return car after driving. 
        /// </summary>
        /// <param name="returnDTO">Return object</param>
        public void ReturnCar(ReturnDTO returnDTO)
        {
            try
            {
                var order = RentUnitOfWork.Orders.Get(returnDTO.Order.Id);
                if ((order.Return == null || order.Return.Count == 0) && order.Confirm != null &&
                    order.Confirm.Count > 0 &&order.Confirm.First().IsConfirmed)
                {
                    var returnCar = new Return();
                    returnCar.ManagerId = returnDTO.User.Id;
                    returnCar.IsReturned = true;
                    returnCar.Order = RentUnitOfWork.Orders.Get(returnDTO.Order.Id);
                    if (returnDTO.Crash != null)
                    {
                        returnCar.Crash = new[] 
                        {
                            new Crash()
                            {
                                Description = returnDTO.Crash.Description
                            }
                        };
                        returnCar.Crash.First().Payment = new[] 
                        {
                            new Payment()
                            {
                                IsPaid = false,
                                Price = returnDTO.Crash.Payment.Price
                            }
                        };
                    }
                    RentUnitOfWork.Returns.Create(returnCar);
                    RentUnitOfWork.Save();
                }
            }
            catch (Exception e)
            {
                CreateLog(e, "ManagerService", "ReturnCar");
            }
        }
    }
}
