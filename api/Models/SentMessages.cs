namespace api.Models
{
    public class SentMessages
    {
        public int Id { get; set; }

        public DateTime DateSent { get; set; }

        public string? Content { get; set; }
    }
}