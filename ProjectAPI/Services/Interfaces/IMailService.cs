using Model.Entities;
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
        Task SendInquiryResponseEmail(string ToEmail, string username, string response, string inquiry, string dateCreated);
        Task SendBookingConfirmationEmail(string ToEmail, string UserName, VehicleBooking vehicle);
        Task SendDMVNotification(string name, byte[] license, string offense, string date, int id, string licenseId);
    }
}
