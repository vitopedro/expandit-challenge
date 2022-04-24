using System.Collections.Generic;

namespace Challenge.Models
{
    public class Contact
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string? Photo { get; set; }
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }

    }
}
