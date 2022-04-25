using System.Threading.Tasks;
using Challenge.Helpers.Pagination;
using Challenge.DTOs;

namespace Challenge.Interfaces
{
    public interface IContactsRepository
    {
        Pagination<ContactDTO> GetAll(
            string name,
            string email,
            string number,
            int pageLimit,
            int currentPage,
            string order
        );

        Task<ContactDTO> GetOne(long id);

        Task<ContactDTO> CreateOne(ContactDTO contactDTO);

        Task<ContactDTO> UpdateOne(long id, ContactDTO contactDTO);

        Task DeleteOne(long id);

        Task Save();
    }
}