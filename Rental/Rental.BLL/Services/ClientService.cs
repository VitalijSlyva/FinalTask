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
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.Services
{
    public class ClientService :Service, IClientService
    {
        public ClientService(IRentMapperDTO mapperDTO, IRentUnitOfWork rentUnit,
                                IIdentityUnitOfWork identityUnit, IIdentityMapperDTO identityMapper,ILogService log)
                : base(mapperDTO, rentUnit, identityUnit, identityMapper,log)
        {

        }

        public void CreatePayment(int id, string transactionId)
        {
            try
            {
                Payment payment = RentUnitOfWork.Payments.Get(id);
                payment.TransactionId = transactionId;
                payment.IsPaid = true;
                RentUnitOfWork.Payments.Update(payment);
                RentUnitOfWork.Save();
            }
            catch (Exception e)
            {
                CreateLog(e, "ClientService", "CreatePayment");
            }
        }
        
        public string GetStatus(int id)
        {
            try
            {
                var test1 = RentUnitOfWork.Returns.Find(x => x.OrderId == id && x.Crash != null&&x.Crash.Count>0);
                if (_answer(test1))
                    return "Возвращен с повреждениями";
                var test2 = RentUnitOfWork.Returns.Find(x => x.OrderId == id);
                if (_answer(test2))
                    return "Возвращен";
                var test3 = RentUnitOfWork.Confirms.Find(x => x.OrderId == id && x.IsConfirmed);
                if (_answer(test3))
                    return "Одобрен";
                var test4 = RentUnitOfWork.Confirms.Find(x => x.OrderId == id && !x.IsConfirmed);
                if (_answer(test4))
                    return "Отклонен (" + test4.First().Description+")";
                return "На рассмотрении";
            }
            catch (Exception e)
            {
                CreateLog(e, "ClientService", "GetStatus");
                return null;
            }
        }

        private bool _answer(IEnumerable<object> data)
        {
            return data != null && data.Count() > 0;
        }

        public async Task CreateProfileAsync(ProfileDTO profileDTO)
        {
            try
            {
                profileDTO.Id = profileDTO.User.Id;
                Profile profile = IdentityMapperDTO.ToProfile.Map<ProfileDTO, Profile>(profileDTO);
                profile.ApplicationUser = IdentityUnitOfWork.UserManager.FindById(profileDTO.User.Id);
                IdentityUnitOfWork.ClientManager.Create(profile);
                IdentityUnitOfWork.Save();
            }
            catch (Exception e)
            {
                CreateLog(e, "ClientService", "CreateProfileAsync");
            }
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersForClientAsync(string userId)
        {
            try
            {
                IEnumerable<Order> orders = RentUnitOfWork.Orders.Find(x => x.ClientId == userId);
                List<OrderDTO> ordersDTO = RentMapperDTO.ToOrderDTO.Map<IEnumerable<Order>, List<OrderDTO>>(orders);
                return ordersDTO;
            }
            catch (Exception e)
            {
                CreateLog(e, "ClientService", "GetOrdersForClientAsync");
                return null;
            }
        }

        public async Task MakeOrderAsync(OrderDTO orderDTO)
        {
            try
            {
                Order order = RentMapperDTO.ToOrder.Map<OrderDTO, Order>(orderDTO);
                order.Car = RentUnitOfWork.Cars.Get(orderDTO.Car.Id);
                order.ClientId = orderDTO.Profile.Id;
                int price =((int)(order.DateEnd - order.DateStart).TotalDays+1) * order.Car.Price+
                    (orderDTO.WithDriver? ((int)(order.DateEnd - order.DateStart).TotalDays + 1) * 300 : 0);
                order.Payment =new[] { new Payment() { IsPaid = false, Price = price }};
                RentUnitOfWork.Orders.Create(order);
                RentUnitOfWork.Save();
            }
            catch (Exception e)
            {
                CreateLog(e, "ClientService", "MakeOrderAsync");
            }
        }

        public IEnumerable<PaymentDTO> GetPaymentsForClient(string id)
        {
            try
            {
                IEnumerable<Payment> payments = RentUnitOfWork.Payments.Find(x => x?.Order?.ClientId == id||x.Crash?.Return?.Order?.ClientId==id);
                List<PaymentDTO> paymentsDTO = RentMapperDTO.ToPaymentDTO.Map<IEnumerable<Payment>, List<PaymentDTO>>(payments);
                return paymentsDTO;
            }
            catch (Exception e)
            {
                CreateLog(e, "ClientService", "GetPaymentsForClient");
                return null;
            }
        }

        public async  Task<ProfileDTO> ShowProfileAsync(string id)
        {
            try
            {
                var profile = (await IdentityUnitOfWork.UserManager.FindByIdAsync(id)).Profile;
                return IdentityMapperDTO.ToProfileDTO.Map<Profile, ProfileDTO>(profile);
            }
            catch (Exception e)
            {
                CreateLog(e, "ClientService", "ShowProfileAsync");
                return null;
            }
        }

        public void UpdateProfile(ProfileDTO profileDTO)
        {
            try
            {
                profileDTO.Id = profileDTO.User.Id;
                Profile profile = IdentityMapperDTO.ToProfile.Map<ProfileDTO, Profile>(profileDTO);
                profile.ApplicationUser = IdentityUnitOfWork.UserManager.FindById(profileDTO.User.Id);
                IdentityUnitOfWork.ClientManager.Update(profile);
                IdentityUnitOfWork.Save();
            }
            catch (Exception e)
            {
                CreateLog(e, "ClientService", "UpdateProfileAsync");
            }
        }

        public PaymentDTO GetPayment(int id)
        {
            try { 
            var paymentDTO = RentUnitOfWork.Payments.Show().FirstOrDefault(x => x.Id == id);
            if (paymentDTO == null)
                return null;
            return RentMapperDTO.ToPaymentDTO.Map<Payment, PaymentDTO>(paymentDTO);
            }
            catch (Exception e)
            {
                CreateLog(e, "ClientService", "GetPayment");
                return null;
            }
        }

        public bool CanCreateOrder(string id)
        {
            var paments = RentUnitOfWork.Payments.Find(x => (x.Order?.ClientId ?? "") == id || (x.Crash?.Return?.Order?.ClientId ?? "") == id).ToList();
            if(paments.Count()>0)
            return paments.All(x =>x.IsPaid);
            return true;
        }


        public bool CarIsFree(int carId, DateTime startDate, DateTime endDate)
        {
            return !RentUnitOfWork.Orders.Show().Any(x => x.CarId == carId && ((x.DateStart.Date >= startDate.Date && x.DateStart.Date <=
            endDate.Date) || (x.DateEnd.Date >= startDate.Date && x.DateEnd.Date <= endDate.Date))&&(x?.Confirm?.FirstOrDefault()?.IsConfirmed??true)!=false);
        }

    }
}
