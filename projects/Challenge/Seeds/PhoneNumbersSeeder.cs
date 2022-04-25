
using System.Collections.Generic;
using System.Linq;
using Challenge.Models;

namespace Challenge.Seeds
{
    public class PhoneNumbersSeeder
    {
        private readonly ChallengeContext _context;

        public PhoneNumbersSeeder(ChallengeContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.PhoneNumbers.Count() == 0) {
                var phoneNumbers = new List<PhoneNumber>()
                {
                    new PhoneNumber()
                    {
                        Id = 1,
                        Number = "910000000",
                        ContactId = 1,
                        Label = "Pessoal",
                    },
                    new PhoneNumber()
                    {
                        Id = 2,
                        Number = "910000000",
                        ContactId = 2,
                        Label = "Pessoal",
                    },
                    new PhoneNumber()
                    {
                        Id = 3,
                        Number = "910000000",
                        ContactId = 3,
                        Label = "Pessoal",
                    },
                    new PhoneNumber()
                    {
                        Id = 4,
                        Number = "910000000",
                        ContactId = 4,
                        Label = "Pessoal",
                    },
                    new PhoneNumber()
                    {
                        Id = 5,
                        Number = "910000000",
                        ContactId = 5,
                        Label = "Pessoal",
                    },
                    new PhoneNumber()
                    {
                        Id = 6,
                        Number = "910000000",
                        ContactId = 6,
                        Label = "Pessoal",
                    },
                    new PhoneNumber()
                    {
                        Id = 7,
                        Number = "910000000",
                        ContactId = 7,
                        Label = "Pessoal",
                    },
                    new PhoneNumber()
                    {
                        Id = 8,
                        Number = "910000000",
                        ContactId = 8,
                        Label = "Pessoal",
                    },
                    new PhoneNumber()
                    {
                        Id = 9,
                        Number = "910000000",
                        ContactId = 9,
                        Label = "Pessoal",
                    },
                    new PhoneNumber()
                    {
                        Id = 10,
                        Number = "910000000",
                        ContactId = 10,
                        Label = "Pessoal",
                    },
                    new PhoneNumber()
                    {
                        Id = 11,
                        Number = "910000000",
                        ContactId = 11,
                        Label = "Pessoal",
                    },
                    new PhoneNumber()
                    {
                        Id = 12,
                        Number = "910000000",
                        ContactId = 12,
                        Label = "Pessoal",
                    },
                    new PhoneNumber()
                    {
                        Id = 13,
                        Number = "910000000",
                        ContactId = 13,
                        Label = "Pessoal",
                    },
                    new PhoneNumber()
                    {
                        Id = 14,
                        Number = "910000000",
                        ContactId = 14,
                        Label = "Pessoal",
                    },
                    new PhoneNumber()
                    {
                        Id = 15,
                        Number = "910000000",
                        ContactId = 15,
                        Label = "Pessoal",
                    },
                };

                _context.PhoneNumbers.AddRange(phoneNumbers);
                _context.SaveChanges();
            }
        }
    }
}
