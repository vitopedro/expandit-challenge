using Challenge.Models;

namespace Challenge.DTOs
{
    public class PhoneNumberDTO
    {
        public string Number { get; set; }
        public string Label { get; set; }

        public PhoneNumberDTO() {}

        public PhoneNumberDTO(PhoneNumber phoneNumber)
        {
            Number = phoneNumber.Number;
            Label = phoneNumber.Label;
        }
    }
}
