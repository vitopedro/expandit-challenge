using System.Threading.Tasks;
using Challenge.Helpers.Pagination;
using Challenge.DTOs;

namespace Challenge.Interfaces
{
    public interface IGroupsRepository
    {
        Pagination<GroupDTO> GetAll(
            string name,
            int pageLimit,
            int currentPage,
            string order
        );

        Task<GroupDTO> GetOne(long id);

        Task<GroupDTO> CreateOne(GroupFormDTO groupFormDTO);

        Task<GroupDTO> UpdateOne(long id, GroupFormDTO groupFormDTO);

        Task DeleteOne(long id);

        Task Save();
    }
}
