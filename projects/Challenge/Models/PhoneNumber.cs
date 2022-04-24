
namespace Challenge.Models
{
    public class PhoneNumber
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public long ContactId { get; set; }
        public string Label { get; set; }
    }
}
