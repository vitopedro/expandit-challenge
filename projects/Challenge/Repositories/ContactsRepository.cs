using System.Threading.Tasks;
using Challenge.Helpers.Pagination;
using Challenge.DTOs;
using Challenge.Models;
using Challenge.Interfaces;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Repositories
{
    public class ContactsRepository: IContactsRepository
    {
        private ChallengeContext _context;

        public ContactsRepository(ChallengeContext context)
        {
            _context = context;
        }

        public Pagination<ContactDTO> GetAll(
            string? name,
            string? email,
            string? number,
            int pageLimit,
            int currentPage,
            string? order
        )
        {
            var query = _context.Contacts.Include(c => c.PhoneNumbers);

            var results = query.Select(c => new ContactDTO(c)).AsEnumerable<ContactDTO>();

            if (!string.IsNullOrEmpty(name)) {
                results = results.Where(c => c.Name.ToLower().Contains(name));
            }

            if (!string.IsNullOrEmpty(email)) {
                results = results.Where(c => c.Email.ToLower().Contains(email));
            }

            /*if (!string.IsNullOrEmpty(number)) {
                var phoneNumberIds = _context.PhoneNumbers
                    .Where(pn => pn.Number.Contains(number))
                    .Select(pn => pn.Id)
                    .AsEnumerable<long>();

                if (phoneNumberIds.Count() == 0) {
                    var paginationHelperEmpty = new PaginationHelper<ContactDTO>(new List<ContactDTO>(), currentPage, pageLimit);
                    return new OkObjectResult(paginationHelperEmpty.GetPagination());
                }

                results = results.Where(c => c.PhoneNumbers.Select(pn => pn.));
            }*/

            if (order == "desc") {
                results = results.OrderByDescending(c => c.Name);
            } else {
                results = results.OrderBy(c => c.Name);
            }

            var paginationHelper = new PaginationHelper<ContactDTO>(results, currentPage, pageLimit);

            return paginationHelper.GetPagination();
        }

        public async Task<ContactDTO> GetOne(long id)
        {
            var contact = await _context.Contacts
                .Include(c => c.PhoneNumbers)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            return new ContactDTO(contact);
        }

        public async Task<ContactDTO> CreateOne(ContactDTO contactDTO)
        {
            var contact = new Contact
            {
                Name = contactDTO.Name,
                Address = contactDTO.Address,
                Email = contactDTO.Email,
                PhoneNumbers = contactDTO.PhoneNumbers.Select(
                    pn => new PhoneNumber{
                        Label = pn.Label,
                        Number = pn.Number
                    }
                ).ToList<PhoneNumber>()
            };

            _context.Contacts.Add(contact);
            await Save();
            return await this.GetOne(contact.Id);
        }

        public async Task<ContactDTO> UpdateOne(long id, ContactDTO contactDTO)
        {
             var contact = await _context.Contacts
                .Include(c => c.PhoneNumbers)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (contact == null) {
                return null;
            }

            contact.Name = contactDTO.Name;
            contact.Address = contactDTO.Address;
            contact.Email = contactDTO.Email;
            contact.PhoneNumbers = contactDTO.PhoneNumbers.Select(
                pn => new PhoneNumber{
                    Label = pn.Label,
                    Number = pn.Number
                }
            ).ToList<PhoneNumber>();
            await Save();
            return await this.GetOne(contact.Id);
        }

        public async Task DeleteOne(long id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) {
                throw new Exception("Not found");
            }

            _context.Contacts.Remove(contact);
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}