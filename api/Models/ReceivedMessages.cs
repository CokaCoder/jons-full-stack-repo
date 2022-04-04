namespace api.Models
{
    public class ReceivedMessages
    {
        public int Id { get; set; }

        public DateTime DateReceived { get; set; }

        public string? Content { get; set; }
    }
}
