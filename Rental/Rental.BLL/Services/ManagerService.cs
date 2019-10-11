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

        public IEnumerable<ConfirmDTO> GetConfirms()
        {
            var confirms = RentUnitOfWork.Confirms.Show();
            return RentMapperDTO.ToConfirmDTO.Map<IEnumerable<Confirm>, List<ConfirmDTO>>(confirms);
        }

        public IEnumerable<ReturnDTO> GetReturns()
        {
            var returns = RentUnitOfWork.Returns.Show();
            return RentMapperDTO.ToReturnDTO.Map<IEnumerable<Return>, List<ReturnDTO>>(returns);
        }

        public ReturnDTO GetReturn(int id)
        {
            var returnObject = RentUnitOfWork.Returns.Get(id);
            return RentMapperDTO.ToReturnDTO.Map<Return,ReturnDTO>(returnObject);
        }

        public ConfirmDTO GetConfirm(int id)
        {
            var confirm = RentUnitOfWork.Confirms.Get(id);
            return RentMapperDTO.ToConfirmDTO.Map<Confirm, ConfirmDTO>(confirm);
        }

        public async Task ConfirmOrder(ConfirmDTO confirmDTO)
        {
            try
            {
                var confirm = RentMapperDTO.ToConfirm.Map<ConfirmDTO, Confirm>(confirmDTO);
                confirm.ManagerId = confirmDTO.User.Id;
                confirm.Order = RentUnitOfWork.Orders.Get(confirmDTO.Order.Id);
                RentUnitOfWork.Confirms.Create(confirm);
                RentUnitOfWork.Save();
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
