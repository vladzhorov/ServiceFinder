using MediatR;
using ServiceFinder.NotificationService.Domain.Interfaces;
using ServiceFinder.NotificationsService.Domain.Models;

namespace ServiceFinder.NotificationsService.Application.EventHandlers
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, Unit>
    {
        private readonly IEmailSender _emailSender;

        public SendEmailCommandHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task<Unit> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            await _emailSender.SendEmailAsync(request.Email, request.Subject, request.Body);
            return Unit.Value;
        }
    }
}