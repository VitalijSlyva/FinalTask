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

        IEnumerable<OrderDTO> GetForReturns();

        IEnumerable<OrderDTO> GetForConfirms();

        OrderDTO GetOrder(int id,bool forConfirm);

    }
}
