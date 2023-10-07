namespace Architecture.Services
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
