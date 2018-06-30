﻿using System.Threading.Tasks;

namespace AppleUsed.BLL.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}