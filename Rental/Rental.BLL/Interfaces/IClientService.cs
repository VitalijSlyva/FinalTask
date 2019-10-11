using Rental.BLL.DTO.Identity;
using Rental.BLL.DTO.Rent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.Interfaces
{
    public interface IClientService:IDisposable
    {
        Task MakeOrderAsync(OrderDTO order);

        Task<IEnumerable<OrderDTO>> GetOrdersForClientAsync(string name);

        void CreatePayment(int id,string transactionId);

        Task CreateProfileAsync(ProfileDTO profileDTO);

        Task UpdateProfileAsync(ProfileDTO profileDTO);

        Task<ProfileDTO> ShowProfileAsync(string id);
    }
}
