namespace GeldMeister.Common.Application.MessageBrokers;

public class MessageMetadata
{
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    public static MessageMetadata Now() => 
        new() {CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now};
}