using System.Linq;
using Challenge.Models;

namespace Challenge.DTOs
{
    public class ContactDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string? Photo { get; set; }
        public PhoneNumberDTO[] PhoneNumbers { get; set; }

        public ContactDTO() {}

        public ContactDTO(Contact contact)
        {
            Id = contact.Id;
            Name = contact.Name;
            Address = contact.Address;
            Email = contact.Email;
            Photo = contact.Photo;
            this.setPhoneNumbers(contact);
        }

        private void setPhoneNumbers(Contact contact)
        {
            if (contact.PhoneNumbers == null) {
                return;
            }

            PhoneNumbers = contact.PhoneNumbers
                .Select(pn => new PhoneNumberDTO(pn))
                .ToArray<PhoneNumberDTO>();
        }
    }
}
