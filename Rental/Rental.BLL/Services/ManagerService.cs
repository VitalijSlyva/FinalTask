using Rental.BLL.Abstracts;
using Rental.BLL.DTO.Rent;
using Rental.BLL.Interfaces;
using Rental.DAL.Entities.Rent;
using Rental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.Services
{
    public class ManagerService :Service, IManagerService
    {
        public ManagerService(IRentMapperDTO mapperDTO, IRentUnitOfWork rentUnit,
                                IIdentityUnitOfWork identityUnit, IIdentityMapperDTO identityMapper,ILogService log)
                : base(mapperDTO, rentUnit, identityUnit, identityMapper,log)
        {

        }

        public IEnumerable<OrderDTO> GetForConfirms()
        {
            try
            {
                var orders = RentUnitOfWork.Orders.Show().Where(x => x.Confirm == null||x.Confirm.Count==0);
                return RentMapperDTO.ToOrderDTO.Map<IEnumerable<Order>, List<OrderDTO>>(orders);
            }
            catch (Exception e)
            {
                CreateLog(e, "ManagerService", "GetForConfirms");
                return null;
            }
        }

        public IEnumerable<OrderDTO> GetForReturns()
        {
            try
            {
                var orders = RentUnitOfWork.Orders.Show().Where(x => x.Confirm != null&&x.Confirm.Count>0 &&( x.Return == null||x.Return.Count==0 )&& x.Confirm.First().IsConfirmed);
                return RentMapperDTO.ToOrderDTO.Map<IEnumerable<Order>, List<OrderDTO>>(orders);
            }
            catch (Exception e)
            {
                CreateLog(e, "ManagerService", "GetForReturns");
                return null;
            }
        }

        public OrderDTO GetOrder(int id,bool forConfirm)
        {
            try
            {
                var order = RentUnitOfWork.Orders.Get(id);
                if (order == null)
                    return null;
                if (forConfirm && order.Confirm != null&&order.Confirm.Count>0)
                    return null;
                if (!forConfirm && ((order.Return != null &&order.Return.Count>0 ) || order.Confirm == null||order.Confirm.Count==0 || !order.Confirm.First().IsConfirmed))
                    return null;
                return RentMapperDTO.ToOrderDTO.Map<Order, OrderDTO>(order);
            }
            catch (Exception e)
            {
                CreateLog(e, "ManagerService", "GetOrder");
                return null;
            }
        }

        public async Task ConfirmOrder(ConfirmDTO confirmDTO)
        {
            try
            {
                if (RentUnitOfWork.Orders.Get(confirmDTO.Order.Id).Confirm == null|| RentUnitOfWork.Orders.Get(confirmDTO.Order.Id).Confirm.Count==0)
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

        public async Task ReturnCar(ReturnDTO returnDTO)
        {
            try
            {
                var order = RentUnitOfWork.Orders.Get(returnDTO.Order.Id);
                if ((order.Return == null || order.Return.Count == 0) && order.Confirm != null && order.Confirm.Count > 0 &&order.Confirm.First().IsConfirmed)
                {
                    var returnCar = new Return();
                    returnCar.ManagerId = returnDTO.User.Id;
                    returnCar.IsReturned = true;
                    returnCar.Order = RentUnitOfWork.Orders.Get(returnDTO.Order.Id);
                    if (returnDTO.Crash != null)
                    {
                        returnCar.Crash = new[] { new Crash() { Description = returnDTO.Crash.Description } };
                        returnCar.Crash.First().Payment = new[] { new Payment() { IsPaid = false, Price = returnDTO.Crash.Payment.Price } };
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
