namespace GlobalTicketManagement.Application.Models.Email
{
    //it is a type not an entity so we dont place in Domain
    public class Email
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}