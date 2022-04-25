
using System.Collections.Generic;
using System.Linq;
using Challenge.Models;

namespace Challenge.Seeds
{
    public class GroupsSeeder
    {
        private readonly ChallengeContext _context;

        public GroupsSeeder(ChallengeContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Groups.Count() == 0) {
                var groups = new List<Group>()
                {
                    new Group()
                    {
                        Id = 1,
                        Name = "Grupo bué de fixe"
                    },
                    new Group()
                    {
                        Id = 2,
                        Name = "Grupo menos fixe"
                    }
                };

                _context.Groups.AddRange(groups);
                _context.SaveChanges();
            }
        }
    }
}
