using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Challenge.Models;
using Challenge.DTOs;
using Challenge.Helpers.Pagination;

namespace Challenge.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ChallengeContext _context;

        public ContactsController(ChallengeContext context)
        {
            _context = context;
        }

        // GET: /contacts
        [HttpGet]
        public async Task<ActionResult<Pagination<IEnumerable<ContactDTO>>>> GetAll(
            [FromQuery] int pageLimit = 10,
            [FromQuery] int currentPage = 0
        )
        {
            var query = _context.Contacts.Include(c => c.PhoneNumbers);

            var results = await query.Select(c => new ContactDTO(c)).ToListAsync();

            var paginationHelper = new PaginationHelper<ContactDTO>(results, currentPage, pageLimit);

            return new OkObjectResult(paginationHelper.GetPagination());
        }

        // GET: /contacts/ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDTO>> GetOne(long id)
        {
            var result = await _context.Contacts
                .Include(c => c.PhoneNumbers)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (result == null) {
                return NotFound();
            }

            return new OkObjectResult(new ContactDTO(result));
        }

        // POST: /contacts
        [HttpPost]
        public async Task<ActionResult<ContactDTO>> CreateOne([FromBody] ContactDTO contactDTO)
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
            var saved = await _context.SaveChangesAsync();

            if (saved <= 0) {
                return new UnprocessableEntityResult();
            }

            return await this.GetOne(contact.Id);
        }

        // PUT: /contacts/ID
        [HttpPut("{id}")]
        public async Task<ActionResult<ContactDTO>> UpdateOne(long id, ContactDTO contactDTO)
        {
            var contact = await _context.Contacts
                .Include(c => c.PhoneNumbers)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (contact == null) {
                return new NotFoundResult();
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

            await _context.SaveChangesAsync();

            return await this.GetOne(id);
        }

        // DELETE: /contacts/ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOne(long id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("testme")]
        public string TextMe()
        {
            return "Hello";
        }
    }
}
