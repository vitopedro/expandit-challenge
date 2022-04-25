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
    public class GroupsController : ControllerBase
    {
        private readonly IGroupsRepository _repository;

        public GroupsController(IGroupsRepository repository)
        {
            _repository = repository;
        }

        // GET: /groups
        [HttpGet]
        public async Task<ActionResult<Pagination<IEnumerable<GroupDTO>>>> GetAll(
            [FromQuery] string? name = null,
            [FromQuery] int pageLimit = 10,
            [FromQuery] int currentPage = 0,
            [FromQuery] string? order = "asc"
        )
        {
            //try
            //{
                var results = _repository.GetAll(
                    name,
                    pageLimit,
                    currentPage,
                    order
                );

                return new OkObjectResult(results);
            /*}
            catch (System.Exception)
            {
                return StatusCode(500);
            }*/
        }

        // GET: /groups/ID
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupDTO>> GetOne(long id)
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

        // POST: /groups
        [HttpPost]
        public async Task<ActionResult<GroupDTO>> CreateOne([FromBody] GroupFormDTO groupFormDTO)
        {
            try
            {
                var group = await _repository.CreateOne(groupFormDTO);
                return new OkObjectResult(group);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        // PUT: /groups/ID
        [HttpPut("{id}")]
        public async Task<ActionResult<GroupDTO>> UpdateOne(long id, GroupFormDTO groupFormDTO)
        {
            try
            {
                var group = await _repository.UpdateOne(id, groupFormDTO);
                if (group == null) {
                    return NotFound();
                }
                return new OkObjectResult(group);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        // DELETE: /groups/ID
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
    }
}
