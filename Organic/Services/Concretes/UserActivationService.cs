
using Organic.Contracts.Email;
using Organic.Contracts.Identity;
using Organic.Database;
using Organic.Database.Models;
using Organic.Exceptions;
using Organic.Services.Abstracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace Organic.Services.Concretes
{
    public class UserActivationService : IUserActivationService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly IUrlHelper _urlHelper;
        private readonly DateTime _activationExpireDate;
        private const string EMAIL_CONFIRMATION_ROUTE_NAME = "client-auth-activate";
        private const string EMAIL_NEWPASSWORD_ROUTE_NAME = "client-auth-password";

        public UserActivationService(
            DataContext dataContext,
            IHttpContextAccessor httpContextAccessor,
            IEmailService emailService,
            IUrlHelper urlHelper,
            IConfiguration configuration)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _urlHelper = urlHelper;

            double activationValidityMonute =
                Convert.ToDouble(configuration.GetRequiredSection("ActivationValidityMinute").Value);

            _activationExpireDate = DateTime.Now.AddMinutes(activationValidityMonute);
        }

        private string GenerateActivationToken()
        {
            return Guid.NewGuid().ToString();
        }

        private string GenerateUrl(string token, string routeName)
        {
            var request = _httpContextAccessor.HttpContext!.Request;
            return _urlHelper.RouteUrl(routeName, new { token = token }, request.Scheme, request.Host.Value)!;
        }

        private async Task<UserActivation> CreateUserActivationAsync(User user, string token, string activationUrL, DateTime expireDate)
        {
            var userActivation = await _dataContext.UserActivations.FirstOrDefaultAsync(u => u.UserId == user.Id);

            if (userActivation is null)
            {
                var useractivation = new UserActivation
                {
                    User = user,
                    ActivationToken = token,
                    ActivationUrl = activationUrL,
                    ExpireDate = expireDate,
                };

                await _dataContext.UserActivations.AddAsync(useractivation);
            }
            else
            {
                userActivation.ActivationUrl = activationUrL;
                userActivation.ActivationToken = token;
                userActivation.ExpireDate = expireDate;

                _dataContext.UserActivations.Update(userActivation);
                await _dataContext.SaveChangesAsync();

            }
            return userActivation;
        }

        public async Task SendActivationUrlAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user);
            var token = GenerateActivationToken();


            if (user.IsEmailConfirmed == true)
            {
                var activationUrl = GenerateUrl(token, EMAIL_NEWPASSWORD_ROUTE_NAME);
                var activationMessageDto = PrepareNewPasswordMessage(user.Email!, activationUrl);
                await CreateUserActivationAsync(user, token, activationUrl, _activationExpireDate);
                _emailService.Send(activationMessageDto);
            }
            else
            {
                var activationUrl = GenerateUrl(token, EMAIL_CONFIRMATION_ROUTE_NAME);
                var activationMessageDto = PrepareActivationMessage(user.Email!, activationUrl);
                await CreateUserActivationAsync(user, token, activationUrl, _activationExpireDate);
                _emailService.Send(activationMessageDto);
            }

            
        }

        private MessageDto PrepareActivationMessage(string email, string activationUrl)
        {
            string body = EmailMessages.Body.ACTIVATION_MESSAGE
                .Replace(EmailMessageKeywords.ACTIVATION_URL, activationUrl);

            string subject = EmailMessages.Subject.ACTIVATION_MESSAGE;

            return new MessageDto(email, subject, body);
        }

        private MessageDto PrepareNewPasswordMessage(string email, string activationUrl)
        {
            string body = EmailMessages.Body.NEWPASSWORD_MESSAGE
                .Replace(EmailMessageKeywords.ACTIVATION_URL, activationUrl);

            string subject = EmailMessages.Subject.NEWPASSWORD_MESSAGE;

            return new MessageDto(email, subject, body);
        }

    }
}
