using Model.Models;
using Model.Models.MailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Services.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task SendWelcomeEmailAsync(WelcomeRequest request);
        Task SendProfileUpdated(ProfileUpdated request);
        Task SendPasswordUpdateConfirmation(string ToEmail, string UserName, string code);
    }
}
