using System.Linq;
using Challenge.Models;

namespace Challenge.DTOs
{
    public class GroupFormDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ContactFormGroupDTO[] Contacts { get; set; }
    }
}
