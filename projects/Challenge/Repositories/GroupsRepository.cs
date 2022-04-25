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
    public class GroupsRepository: IGroupsRepository
    {
        private ChallengeContext _context;

        public GroupsRepository(ChallengeContext context)
        {
            _context = context;
        }

        public Pagination<GroupDTO> GetAll(
            string? name = null,
            int pageLimit = 10,
            int currentPage = 0,
            string? order = "asc"
        )
        {
            var query = _context.Groups
                .Include(g => g.ContactGroups)
                .ThenInclude(cg => cg.Contact);

            var results = query.Select(g => new GroupDTO(g)).AsEnumerable<GroupDTO>();

            if (!string.IsNullOrEmpty(name)) {
                results = results.Where(g => g.Name.ToLower().Contains(name));
            }

            if (order == "desc") {
                results = results.OrderByDescending(c => c.Name);
            } else {
                results = results.OrderBy(c => c.Name);
            }

            var paginationHelper = new PaginationHelper<GroupDTO>(results, currentPage, pageLimit);

            return paginationHelper.GetPagination();
        }

        public async Task<GroupDTO> GetOne(long id)
        {
            var group = await _context.Groups
                .Include(g => g.ContactGroups)
                .ThenInclude(cg => cg.Contact)
                .Where(g => g.Id == id)
                .FirstOrDefaultAsync();

            return new GroupDTO(group);
        }

        public async Task<GroupDTO> CreateOne(GroupFormDTO groupFormDTO)
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
            await Save();

            return await this.GetOne(group.Id);
        }

        public async Task<GroupDTO> UpdateOne(long id, GroupFormDTO groupFormDTO)
        {
            var group = await _context.Groups
                .Include(g => g.ContactGroups)
                .Where(g => g.Id == id)
                .FirstOrDefaultAsync();

            if (group == null) {
                return null;
            }

            group.Name = groupFormDTO.Name;
            group.ContactGroups = groupFormDTO.Contacts.Select(
                c => new ContactGroup{
                    ContactId = c.Id
                }
            ).ToList<ContactGroup>();
            await Save();

            return await this.GetOne(id);
        }

        public async Task DeleteOne(long id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null) {
                throw new Exception("Not found");
            }

            _context.Groups.Remove(group);
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
