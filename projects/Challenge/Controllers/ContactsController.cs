using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Challenge.DTOs;
using Challenge.Helpers.Pagination;
using Challenge.Interfaces;

namespace Challenge.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsRepository _repository;

        public ContactsController(IContactsRepository repository)
        {
            _repository = repository;
        }

        // GET: /contacts
        [HttpGet]
        public async Task<ActionResult<Pagination<IEnumerable<ContactDTO>>>> GetAll(
            [FromQuery] string? name = null,
            [FromQuery] string? email = null,
            [FromQuery] string? number = null,
            [FromQuery] int pageLimit = 10,
            [FromQuery] int currentPage = 0,
            [FromQuery] string? order = "asc"
        )
        {
            try
            {
                var results = _repository.GetAll(
                    name,
                    email,
                    number,
                    pageLimit,
                    currentPage,
                    order
                );

                return new OkObjectResult(results);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        // GET: /contacts/ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDTO>> GetOne(long id)
        {
            try
            {
                var result = await _repository.GetOne(id);
                if (result == null) {
                    return NotFound();
                }
                return new OkObjectResult(result);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        // POST: /contacts
        [HttpPost]
        public async Task<ActionResult<ContactDTO>> CreateOne([FromBody] ContactDTO contactDTO)
        {
            try
            {
                var contact = await _repository.CreateOne(contactDTO);
                return new OkObjectResult(contact);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        // PUT: /contacts/ID
        [HttpPut("{id}")]
        public async Task<ActionResult<ContactDTO>> UpdateOne(long id, ContactDTO contactDTO)
        {
            try
            {
                var contact = await _repository.UpdateOne(id, contactDTO);
                if (contact == null) {
                    return NotFound();
                }
                return new OkObjectResult(contact);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        // DELETE: /contacts/ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOne(long id)
        {
            try
            {
                await _repository.DeleteOne(id);
                return NoContent();
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("testme")]
        public string TestMe()
        {
            return "Hello";
        }
    }
}
