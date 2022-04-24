using System.Linq;
using Challenge.Models;

namespace Challenge.DTOs
{
    public class GroupDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ContactDTO[] Contacts { get; set; }

        public GroupDTO() {}

        public GroupDTO(Group group)
        {
            Id = group.Id;
            Name = group.Name;
            this.setContacts(group);
        }

        private void setContacts(Group group)
        {
            if (group.ContactGroups == null) {
                return;
            }

            Contacts = group.ContactGroups
                .Select(cg => new ContactDTO(cg.Contact))
                .ToArray<ContactDTO>();
        }
    }
}
