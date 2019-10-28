using Microsoft.AspNet.Identity;
using Rental.BLL.Abstracts;
using Rental.BLL.DTO.Identity;
using Rental.BLL.Interfaces;
using Rental.DAL.Entities.Identity;
using Rental.DAL.Interfaces;
using System;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rental.BLL.Services
{
    /// <summary>
    /// Service for account actions.
    /// </summary>
    public class AccountService : Service, IAccountService
    {
        /// <summary>
        /// Create units and mappers for work.
        /// </summary>
        /// <param name="mapperDTO">Mapper for converting database entities to DTO entities</param>
        /// <param name="rentUnit">Rent unit of work</param>
        /// <param name="identityUnit">Udentity unit of work</param>
        /// <param name="identityMapper">Mapper for converting identity entities to BLL classes</param>
        /// <param name="log">Service for logging</param>
        public AccountService(IRentMapperDTO mapperDTO, IRentUnitOfWork rentUnit,
                 IIdentityUnitOfWork identityUnit, IIdentityMapperDTO identityMapper, ILogService log)
                     :base(mapperDTO, rentUnit, identityUnit, identityMapper,log)
        {

        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="client">User object</param>
        /// <returns>Claims information</returns>
        public async Task<ClaimsIdentity> AuthenticateAsync(User client)
        {
            try
            {
                ClaimsIdentity claims = null;
                ApplicationUser user = await IdentityUnitOfWork.UserManager.FindAsync(client.Email, client.Password);
                if (user != null)
                    claims = await IdentityUnitOfWork.UserManager
                        .CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                return claims;
            }
            catch(Exception e)
            {
                CreateLog(e, "AccountService", "AuthenticateAsync");
                return null;
            }
        }

        /// <summary>
        /// Create new user.
        /// </summary>
        /// <param name="client">Use object</param>
        /// <returns>Status</returns>
        public async Task<string> CreateAsync(User client)
        {
            try
            {
                ApplicationUser user = await IdentityUnitOfWork.UserManager.FindByEmailAsync(client.Email);
                if (user == null)
                {
                    user = new ApplicationUser() {UserName=client.Email, Email = client.Email,Name=client.Name };
                    var result = await IdentityUnitOfWork.UserManager.CreateAsync(user, client.Password);
                    if (result.Errors.Count() > 0)
                        return result.Errors.FirstOrDefault();
                    var role = await IdentityUnitOfWork.RoleManager.FindByNameAsync("client");
                    if (role == null)
                    {
                        role = new ApplicationRole { Name = "client" };
                        await IdentityUnitOfWork.RoleManager.CreateAsync(role);
                    }
                    await IdentityUnitOfWork.UserManager.AddToRoleAsync(user.Id, "client");

                    return "";
                }
                else
                {
                    return "Пользователь уже существует;";
                }
            }
            catch(Exception e)
            {
                CreateLog(e, "AccountService", "CreateAsync");
            }
            return "Произошла ошибка";
        }

        /// <summary>
        /// Get user by id async.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User</returns>
        public async Task<User> GetUserAsync(string id)
        {
            try
            {
                ApplicationUser user = (await IdentityUnitOfWork.UserManager.FindByIdAsync(id));
                if (user != null)
                    return IdentityMapperDTO.ToUserDTO.Map<ApplicationUser, User>(user);

                return null;
            }
            catch (Exception e)
            {
                CreateLog(e, "AccountService", "GetUserAsync");

                return null;
            }
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User</returns>
        public User GetUser(string id)
        {
            try
            {
                ApplicationUser user = IdentityUnitOfWork.UserManager.FindById(id);
                if (user != null)
                    return IdentityMapperDTO.ToUserDTO.Map<ApplicationUser, User>(user);

                return null;
            }
            catch (Exception e)
            {
                CreateLog(e, "AccountService", "GetUserAsync");

                return null;
            }
        }

        /// <summary>
        /// Test on ban for user.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Ban</returns>
        public bool IsBanned(string id)
        {
            return IdentityUnitOfWork.UserManager.IsInRole(id, "banned");
        }

        /// <summary>
        /// Get user id by email.
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Id</returns>
        public string GetIdByEmail(string email)
        {
            try
            {
                var user = IdentityUnitOfWork.UserManager.FindByEmail(email);
                return user?.Id;
            }
            catch(Exception e)
            {
                CreateLog(e, "AccountService", "GetEmailByIdAsync");

                return null;
            }
        }

        /// <summary>
        /// Confirm user email.
        /// </summary>
        /// <param name="userId">Id</param>
        public void ConfirmEmail(string userId)
        {
            try
            {
                var user = IdentityUnitOfWork.UserManager.FindById(userId);
                if (user != null) {
                    user.EmailConfirmed = true;
                    IdentityUnitOfWork.UserManager.Update(user);
                    IdentityUnitOfWork.Save();
                }
            }
            catch (Exception e)
            {
                CreateLog(e, "AccountService", "ConfirmEmailAsync");
            }
        }

        /// <summary>
        /// Change email.
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="userId">Id</param>
        /// <param name="password">Password</param>
        /// <returns>Status</returns>
        public string ChangeEmail(string email,string userId,string password)
        {
            try
            {
                var user = IdentityUnitOfWork.UserManager.FindById(userId);
                var test = IdentityUnitOfWork.UserManager.FindByEmail(email);
                if (test==null&&user != null&&IdentityUnitOfWork.UserManager.CheckPassword(user,password))
                {
                    user.EmailConfirmed = false;
                    user.Email = email;
                    user.UserName = email;
                    IdentityUnitOfWork.UserManager.Update(user);
                    IdentityUnitOfWork.Save();
                    return "";
                }
                else
                {
                    return "Неверный пароль или пользователь с такой почтой уже существует.";
                }
            }
            catch(Exception e)
            {
                CreateLog(e, "AccountService", "ChangeEmail");

                return "Неверный пароль или пользователь с такой почтой уже существует.";
            }
        }

        /// <summary>
        /// Change name.
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="userId">Id</param>
        /// <param name="password">Password</param>
        /// <returns>Status</returns>
        public string ChangeName(string name, string userId, string password)
        {
            try
            {
                var user = IdentityUnitOfWork.UserManager.FindById(userId);
                if (user != null && IdentityUnitOfWork.UserManager.CheckPassword(user, password))
                {
                    user.Name = name;
                    IdentityUnitOfWork.UserManager.Update(user);
                    IdentityUnitOfWork.Save();
                    return "";
                }
                else
                {
                    return "Неверный пароль.";
                }
            }
            catch (Exception e)
            {
                CreateLog(e, "AccountService", "ChangeName");

                return "Неверный пароль";
            }
        }

        /// <summary>
        /// Change password.
        /// </summary>
        /// <param name="userId">Id</param>
        /// <param name="password">Password</param>
        /// <returns>Status</returns>
        public string ChangePassword(string userId, string password)
        {
            try
            {
                var user = IdentityUnitOfWork.UserManager.FindById(userId);
                if (user != null)
                {
                    IdentityUnitOfWork.UserManager.RemovePassword(userId);
                    IdentityUnitOfWork.UserManager.AddPassword(userId, password);
                    IdentityUnitOfWork.Save();
                    return "";
                }
                else
                {
                    return "Неверный пароль.";
                }
            }
            catch (Exception e)
            {
                CreateLog(e, "AccountService", "ChangeName");

                return "Неверный пароль";
            }
        }

        /// <summary>
        /// Send mail.
        /// </summary>
        /// <param name="to">Email</param>
        /// <param name="subject">Email subject</param>
        /// <param name="body">Email body</param>
        /// <param name="isBodyHtml">Body contains html</param>
        public void SendMail(string to, string subject, string body, bool isBodyHtml=true)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("cardoorrental@gmail.com", "Cardoor");
                mailMessage.To.Add(to);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = isBodyHtml;
                client.Send(mailMessage);
            }
            catch (Exception e)
            {
                CreateLog(e, "AccountService", "SendEmai");
            }
        }
    }
}
