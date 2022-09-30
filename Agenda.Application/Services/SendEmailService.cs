using System.Net;
using System.Net.Mail;
using Agenda.Application.Exceptions;
using Agenda.Application.Interfaces;
using Agenda.Application.ViewModels.Email;
using Agenda.Domain.Models;
using AutoMapper;
using FluentValidation;

namespace Agenda.Application.Services
{
    public class SendEmailService : ISendEmailService
    {
        private readonly IMapper _mapper;
        private readonly IValidator<EmailRequest> _validator;

        public SendEmailService(
            IMapper mapper,
            IValidator<EmailRequest> validator
        )
        {
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<EmailResponse> Send(EmailRequest emailRequest)
        {
            var validation = await _validator.ValidateAsync(emailRequest);
            if (!validation.IsValid)
                throw new BadRequestException(validation);

            var email = _mapper.Map<Email>(emailRequest);

            MailMessage emailMessage = new MailMessage();
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Timeout = 60 * 60;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("wesley.lp1407@gmail.com", "senha");

                emailMessage.From = new MailAddress("wesley.lp1407@gmail.com", "Marcelo Wesley");

                emailMessage.Body = emailRequest.Body;
                emailMessage.Subject = emailRequest.Subject;

                emailMessage.IsBodyHtml = true;
                emailMessage.Priority = MailPriority.Normal;
                emailMessage.To.Add(emailRequest.To);

                smtpClient.Send(emailMessage);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return _mapper.Map<EmailResponse>(email);
            
        }
    }
}