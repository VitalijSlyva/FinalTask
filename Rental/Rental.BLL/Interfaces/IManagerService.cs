using Rental.BLL.DTO.Rent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.Interfaces
{
    public interface IManagerService:IDisposable
    {
        Task ConfirmOrder(ConfirmDTO confirm);

        Task ReturnCar(ReturnDTO returnDTO);

        IEnumerable<ReturnDTO> GetReturns();

        IEnumerable<ConfirmDTO> GetConfirms();

        ReturnDTO GetReturn(int id);

        ConfirmDTO GetConfirm(int id);

    }
}
