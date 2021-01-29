﻿using AbpFramework.Net.Mail;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
namespace Abp.MailKit
{
    public class MailKitEmailSender : EmailSenderBase
    {
        #region 实例
        private readonly IMailKitSmtpBuilder _smtpBuilder;
        #endregion
        #region 构造函数
        public MailKitEmailSender(
           IEmailSenderConfiguration smtpEmailSenderConfiguration,
           IMailKitSmtpBuilder smtpBuilder)
           : base(
                 smtpEmailSenderConfiguration)
        {
            _smtpBuilder = smtpBuilder;
        }
        #endregion
        #region 方法
        public override async Task SendAsync(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            using (var client = BuildSmtpClient())
            {
                var message= BuildMimeMessage(from, to, subject, body, isBodyHtml);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
        public override void Send(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            using (var client = BuildSmtpClient())
            {
                var message = BuildMimeMessage(from, to, subject, body, isBodyHtml);
                client.Send(message);
                client.Disconnect(true);
            }
        }
        protected override void SendEmail(MailMessage mail)
        {
            using (var client = BuildSmtpClient())
            {
                var message = mail.ToMimeMessage();
                client.Send(message);
                client.Disconnect(true);
            }
        }

        protected override async Task SendEmailAsync(MailMessage mail)
        {
            using (var client = BuildSmtpClient())
            {
                var message = mail.ToMimeMessage();
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
        protected virtual SmtpClient BuildSmtpClient()
        {
            return _smtpBuilder.Build();
        }
        private static MimeMessage BuildMimeMessage(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            var bodyType = isBodyHtml ? "html" : "plain";
            var message = new MimeMessage
            {
                Subject = subject,
                Body = new TextPart(bodyType)
                {
                    Text = body
                }
            };

            message.From.Add(new MailboxAddress(from));
            message.To.Add(new MailboxAddress(to));

            return message;
        }
        #endregion

    }
}