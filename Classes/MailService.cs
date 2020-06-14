﻿using System;
using System.Net;
using System.Net.Mail;
using store_car_web_project.Classes;

namespace store_car_web_project.Classes
{
    public class MailService
    {
        private readonly string From = "mohsenkadm123@gmail.com";
        private readonly string Pass = "mhmdmhsnali";
       
        public string SendMail(string To, string Subject, string Body)
        {
            MailMessage Message = new MailMessage
            {
                Subject = Subject,
                Body = Body,
                From = new MailAddress(From),
                IsBodyHtml = true,
                Priority = MailPriority.High
            };

            SmtpClient smtp = new SmtpClient
            {
                Port = 587,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(From, Pass)
            };

            Message.To.Add(new MailAddress(To));

            try
            {
                smtp.Send(Message);

                return "تم إرسال البريد بِنجاح";
            }
            catch (Exception ex)
            {
                new Logger().Write(ex, "Send Mail");
                throw;
            }
        }
    }
}