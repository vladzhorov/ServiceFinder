using MediatR;

namespace ServiceFinder.NotificationsService.Domain.Events
{
    public class SendEmailCommand : IRequest<Unit>
    {
        public string Email { get; }
        public string Subject { get; }
        public string Body { get; }

        public SendEmailCommand(string email, string subject, string body)
        {
            Email = email;
            Subject = subject;
            Body = body;
        }
    }
}