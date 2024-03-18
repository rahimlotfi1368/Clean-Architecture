namespace GlobalTicketManagement.Api.Utility.Attributes
{
    public class FileResultContentTypeAttribute:Attribute
    {
        public FileResultContentTypeAttribute(string contentType)
        {
            ContentType = contentType;
        }

        public string ContentType { get; }
    }
}
