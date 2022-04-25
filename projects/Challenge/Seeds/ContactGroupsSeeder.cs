
using System.Collections.Generic;
using System.Linq;
using Challenge.Models;

namespace Challenge.Seeds
{
    public class ContactGroupsSeeder
    {
        private readonly ChallengeContext _context;

        public ContactGroupsSeeder(ChallengeContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.ContactGroups.Count() == 0) {
                var contactGroups = new List<ContactGroup>()
                {
                    new ContactGroup()
                    {
                        GroupId = 1,
                        ContactId = 1,
                    },
                    new ContactGroup()
                    {
                        GroupId = 1,
                        ContactId = 2,
                    },
                    new ContactGroup()
                    {
                        GroupId = 1,
                        ContactId = 3,
                    },
                    new ContactGroup()
                    {
                        GroupId = 1,
                        ContactId = 4,
                    },
                    new ContactGroup()
                    {
                        GroupId = 1,
                        ContactId = 5,
                    },
                    new ContactGroup()
                    {
                        GroupId = 2,
                        ContactId = 10,
                    },
                    new ContactGroup()
                    {
                        GroupId = 2,
                        ContactId = 11,
                    },
                    new ContactGroup()
                    {
                        GroupId = 2,
                        ContactId = 12,
                    },
                    new ContactGroup()
                    {
                        GroupId = 2,
                        ContactId = 13,
                    },
                    new ContactGroup()
                    {
                        GroupId = 2,
                        ContactId = 14,
                    },
                };

                _context.ContactGroups.AddRange(contactGroups);
                _context.SaveChanges();
            }
        }
    }
}
