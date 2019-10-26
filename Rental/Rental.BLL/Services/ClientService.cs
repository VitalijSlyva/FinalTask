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
using System.Threading.Tasks;

namespace Rental.BLL.Services
{
    /// <summary>
    /// Service for client actions.
    /// </summary>
    public class ClientService :Service, IClientService
    {
        /// <summary>
        /// Create units and mappers for work.
        /// </summary>
        /// <param name="mapperDTO">Mapper for converting database entities to DTO entities</param>
        /// <param name="rentUnit">Rent unit of work</param>
        /// <param name="identityUnit">Udentity unit of work</param>
        /// <param name="identityMapper">Mapper for converting identity entities to BLL classes</param>
        /// <param name="log">Service for logging</param>
        public ClientService(IRentMapperDTO mapperDTO, IRentUnitOfWork rentUnit,
                IIdentityUnitOfWork identityUnit, IIdentityMapperDTO identityMapper,ILogService log)
                 : base(mapperDTO, rentUnit, identityUnit, identityMapper,log)
        {

        }

        /// <summary>
        /// Create payment.
        /// </summary>
        /// <param name="id">Client id</param>
        /// <param name="transactionId">Transaction id</param>
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

        /// <summary>
        /// Get status for order.
        /// </summary>
        /// <param name="id">Order id</param>
        /// <returns>Status</returns>
        public string GetStatus(int id)
        {
            try
            {
                bool isNotEmpty(IEnumerable<object> data)=> data != null && data.Count() > 0;

                var test1 = RentUnitOfWork.Returns.Find(x => x.OrderId == id && x.Crash != null&&x.Crash.Count>0);
                if (isNotEmpty(test1))
                    return "Возвращен с повреждениями";

                var test2 = RentUnitOfWork.Returns.Find(x => x.OrderId == id);
                if (isNotEmpty(test2))
                    return "Возвращен";

                var test3 = RentUnitOfWork.Confirms.Find(x => x.OrderId == id && x.IsConfirmed);
                if (isNotEmpty(test3))
                    return "Одобрен";

                var test4 = RentUnitOfWork.Confirms.Find(x => x.OrderId == id && !x.IsConfirmed);
                if (isNotEmpty(test4))
                    return "Отклонен (" + test4.First().Description+")";

                return "На рассмотрении";
            }
            catch (Exception e)
            {
                CreateLog(e, "ClientService", "GetStatus");

                return null;
            }
        }

        /// <summary>
        /// Create information about client.
        /// </summary>
        /// <param name="profileDTO">Profile object</param>
        public void CreateProfile(ProfileDTO profileDTO)
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

        /// <summary>
        /// Get order by client id.
        /// </summary>
        /// <param name="userId">Client id</param>
        /// <returns>Orders</returns>
        public IEnumerable<OrderDTO> GetOrdersForClient(string userId)
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

        /// <summary>
        /// Make order.
        /// </summary>
        /// <param name="orderDTO">Order object</param>
        public void MakeOrder(OrderDTO orderDTO)
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


        /// <summary>
        /// Get payments by client id.
        /// </summary>
        /// <param name="id">Client id</param>
        /// <returns>Payments</returns>
        public IEnumerable<PaymentDTO> GetPaymentsForClient(string id)
        {
            try
            {
                IEnumerable<Payment> payments = RentUnitOfWork.Payments.Find(x => x?.Order?.ClientId == id||
                x.Crash?.Return?.Order?.ClientId==id);
                List<PaymentDTO> paymentsDTO = RentMapperDTO.ToPaymentDTO
                    .Map<IEnumerable<Payment>, List<PaymentDTO>>(payments);
                return paymentsDTO;
            }
            catch (Exception e)
            {
                CreateLog(e, "ClientService", "GetPaymentsForClient");

                return null;
            }
        }

        /// <summary>
        /// Showm information by id.
        /// </summary>
        /// <param name="id">Client id</param>
        /// <returns>Profile</returns>
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

        /// <summary>
        /// Update information about client.
        /// </summary>
        /// <param name="profileDTO">New profile object</param>
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

        /// <summary>
        /// Get payment by id.
        /// </summary>
        /// <param name="id">Payment id</param>
        /// <returns>Payment</returns>
        public PaymentDTO GetPayment(int id)
        {
            try
            {
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

        /// <summary>
        /// Client can create order.
        /// </summary>
        /// <param name="id">Client id</param>
        /// <returns>Can make order</returns>
        public bool CanCreateOrder(string id)
        {
            var paments = RentUnitOfWork.Payments.Find(x => (x.Order?.ClientId ?? "") == id ||
                (x.Crash?.Return?.Order?.ClientId ?? "") == id).ToList();
            if (paments.Count() > 0)
                return paments.All(x => x.IsPaid);

            return true;
        }

        /// <summary>
        /// Car is free for order.
        /// </summary>
        /// <param name="carId">Car id</param>
        /// <param name="startDate">Date start order</param>
        /// <param name="endDate">Date end order</param>
        /// <returns>Is free</returns>
        public bool CarIsFree(int carId, DateTime startDate, DateTime endDate)
        {
            return !RentUnitOfWork.Orders.Show()
                .Any(x => x.CarId == carId && ((x.DateStart.Date <= startDate.Date && x.DateEnd.Date >= startDate.Date) ||
                (x.DateStart.Date <= endDate.Date && x.DateEnd.Date >= endDate.Date))&&(x?.Confirm?.FirstOrDefault()
                ?.IsConfirmed??true)!=false);
        }

    }
}
