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
                                IIdentityUnitOfWork identityUnit, IIdentityMapperDTO identityMapper)
                : base(mapperDTO, rentUnit, identityUnit, identityMapper)
        {

        }

        public IEnumerable<OrderDTO> GetForConfirms()
        {
            var orders = RentUnitOfWork.Orders.Show().Where(x=>x.Confirm==null);
            return RentMapperDTO.ToOrderDTO.Map<IEnumerable<Order>, List<OrderDTO>>(orders);
        }

        public IEnumerable<OrderDTO> GetForReturns()
        {
            var orders = RentUnitOfWork.Orders.Show().Where(x => x.Confirm != null&&x.Return==null&&x.Confirm.IsConfirmed);
            return RentMapperDTO.ToOrderDTO.Map<IEnumerable<Order>, List<OrderDTO>>(orders);
        }

        public OrderDTO GetOrder(int id,bool forConfirm)
        {
            var order = RentUnitOfWork.Orders.Get(id);
            if (order == null)
                return null;
            if (forConfirm && order.Confirm != null)
                return null;
            if (!forConfirm && (order.Return != null || order.Confirm == null || !order.Confirm.IsConfirmed))
                return null;
            return RentMapperDTO.ToOrderDTO.Map<Order, OrderDTO>(order);
        }

        public async Task ConfirmOrder(ConfirmDTO confirmDTO)
        {
            try
            {
                if (RentUnitOfWork.Orders.Get(confirmDTO.Order.Id).Confirm == null)
                {
                    var confirm = RentMapperDTO.ToConfirm.Map<ConfirmDTO, Confirm>(confirmDTO);
                    confirm.ManagerId = confirmDTO.User.Id;
                    confirm.Order = RentUnitOfWork.Orders.Get(confirmDTO.Order.Id);
                    RentUnitOfWork.Confirms.Create(confirm);
                    RentUnitOfWork.Save();
                }
            }
            catch
            {

            }
        }

        public async Task ReturnCar(ReturnDTO returnDTO)
        {
            try
            {
                var returnCar= new Return();
                returnCar.ManagerId = returnDTO.User.Id;
                returnCar.IsReturned = true;
                returnCar.Order = RentUnitOfWork.Orders.Get(returnDTO.Order.Id);
                if (returnDTO.Crash != null)
                {
                    returnCar.Crash = new Crash() { Description = returnCar.Crash.Description };
                    returnCar.Crash.Payment = new Payment() { IsPaid = false, Price = returnCar.Crash.Payment.Price };
                }
                RentUnitOfWork.Returns.Create(returnCar);
                RentUnitOfWork.Save();
            }
            catch
            {

            }
        }
    }
}
