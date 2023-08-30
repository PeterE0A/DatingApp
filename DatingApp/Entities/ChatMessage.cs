namespace DatingApp.Entities
{
    public class ChatMessage
    {
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string MessageContent { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
