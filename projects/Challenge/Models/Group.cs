using System.Collections.Generic;

namespace Challenge.Models
{
    public class Group
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ContactGroup> ContactGroups { get; set; }
    }
}
