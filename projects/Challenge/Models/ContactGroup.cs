
namespace Challenge.Models
{
    public class ContactGroup
    {
        public long Id { get; set; }
        public long ContactId { get; set; }
        public long GroupId { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual Group Group { get; set; }
    }
}
