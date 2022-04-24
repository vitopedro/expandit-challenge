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
    public class GroupsController : ControllerBase
    {
        private readonly ChallengeContext _context;

        public GroupsController(ChallengeContext context)
        {
            _context = context;
        }

        // GET: /groups
        [HttpGet]
        public async Task<ActionResult<Pagination<IEnumerable<GroupDTO>>>> GetAll(
            [FromQuery] int pageLimit = 10,
            [FromQuery] int currentPage = 0
        )
        {
            var query = _context.Groups
                .Include(g => g.ContactGroups)
                .ThenInclude(cg => cg.Contact);

            var results = await query.Select(g => new GroupDTO(g)).ToListAsync();

            var paginationHelper = new PaginationHelper<GroupDTO>(results, currentPage, pageLimit);

            return new OkObjectResult(paginationHelper.GetPagination());
        }

        // GET: /groups/ID
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupDTO>> GetOne(long id)
        {
            var result = await _context.Groups
                .Include(g => g.ContactGroups)
                .ThenInclude(cg => cg.Contact)
                .Where(g => g.Id == id)
                .FirstOrDefaultAsync();

            if (result == null) {
                return NotFound();
            }

            return new OkObjectResult(new GroupDTO(result));
        }

        // POST: /groups
        [HttpPost]
        public async Task<ActionResult<GroupDTO>> CreateOne([FromBody] GroupFormDTO groupFormDTO)
        {
            var group = new Group
            {
                Name = groupFormDTO.Name,
                ContactGroups = groupFormDTO.Contacts.Select(
                    c => new ContactGroup{
                        ContactId = c.Id
                    }
                ).ToList<ContactGroup>()
            };

            _context.Groups.Add(group);
            var saved = await _context.SaveChangesAsync();

            if (saved <= 0) {
                return new UnprocessableEntityResult();
            }

            return await this.GetOne(group.Id);
        }

        // PUT: /groups/ID
        [HttpPut("{id}")]
        public async Task<ActionResult<GroupDTO>> UpdateOne(long id, GroupFormDTO groupFormDTO)
        {
            var group = await _context.Groups
                .Include(g => g.ContactGroups)
                .Where(g => g.Id == id)
                .FirstOrDefaultAsync();

            if (group == null) {
                return new NotFoundResult();
            }

            group.Name = groupFormDTO.Name;
            group.ContactGroups = groupFormDTO.Contacts.Select(
                c => new ContactGroup{
                    ContactId = c.Id
                }
            ).ToList<ContactGroup>();

            await _context.SaveChangesAsync();

            return await this.GetOne(id);
        }

        // DELETE: /groups/ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOne(long id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null) {
                return NotFound();
            }

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
