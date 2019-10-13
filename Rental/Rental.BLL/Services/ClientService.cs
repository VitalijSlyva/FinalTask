﻿using Rental.BLL.Abstracts;
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
                                IIdentityUnitOfWork identityUnit, IIdentityMapperDTO identityMapper)
                : base(mapperDTO, rentUnit, identityUnit, identityMapper)
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
            catch
            {

            }
        }
        
        public string GetStatus(int id)
        {
            var test1 = RentUnitOfWork.Returns.Find(x => x.OrderId == id && x.Crash != null);
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
                return "Отклонен по причине:"+test4.First().Description;
            return "На рассмотрении";
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
                profile.ApplicationUser = await IdentityUnitOfWork.UserManager.FindByIdAsync(profileDTO.User.Id);
                IdentityUnitOfWork.ClientManager.Create(profile);
                IdentityUnitOfWork.Save();
            }
            catch
            {

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
            catch
            {
                return null;
            }
        }

        public async Task MakeOrderAsync(OrderDTO orderDTO)
        {
            try
            {
                Order order = RentMapperDTO.ToOrder.Map<OrderDTO, Order>(orderDTO);
                order.ClientId = orderDTO.Profile.Id;
                int price =(int)(order.DateEnd - order.DateStart).TotalDays * order.Car.Price;
                order.Payment = new Payment() { IsPaid = false, Price = price };
                RentUnitOfWork.Orders.Create(order);
                RentUnitOfWork.Save();
            }
            catch
            {
            }
        }

        public IEnumerable<PaymentDTO> GetPaymentsForClient(string id)
        {
            try
            {
                IEnumerable<Payment> payments = RentUnitOfWork.Payments.Find(x => x.Order.ClientId == id);
                List<PaymentDTO> paymentsDTO = RentMapperDTO.ToPaymentDTO.Map<IEnumerable<Payment>, List<PaymentDTO>>(payments);
                return paymentsDTO;
            }
            catch
            {
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
            catch
            {

            }
            return null;
        }

        public async Task UpdateProfileAsync(ProfileDTO profileDTO)
        {
            try
            {
                profileDTO.Id = profileDTO.User.Id;
                Profile profile = IdentityMapperDTO.ToProfile.Map<ProfileDTO, Profile>(profileDTO);
                profile.ApplicationUser = await IdentityUnitOfWork.UserManager.FindByIdAsync(profileDTO.User.Id);
                IdentityUnitOfWork.ClientManager.Update(profile);
                IdentityUnitOfWork.Save();
            }
            catch
            {

            }
        }

        public PaymentDTO GetPayment(int id)
        {
            var paymentDTO = RentUnitOfWork.Payments.Show().FirstOrDefault(x => x.Id == id);
            if (paymentDTO == null)
                return null;
            return RentMapperDTO.ToPaymentDTO.Map<Payment, PaymentDTO>(paymentDTO);
        }
    }
}
