using System.Collections.Generic;
using System.Linq;

namespace Challenge.Helpers.Pagination
{
    public class PaginationHelper<T>
    {
        private IEnumerable<T> data;
        private Pagination<T> pagination = new Pagination<T>();
        public PaginationHelper(IEnumerable<T> data, int currentPage = 0, int pageLimit = 0)
        {
            this.data = data;
            this.SetNumberOfTotalResults(data.Count());
            this.SetLimit(pageLimit);
            this.SetCurrentPage(currentPage);
            this.SetData(data);
        }

        private void SetNumberOfTotalResults(int numOfTotalResults)
        {
            this.pagination.NumOfTotalResults = numOfTotalResults;
        }

        private void SetData(IEnumerable<T> data)
        {
            this.pagination.Data = data.Skip(this.pagination.CurrentPage * this.pagination.PageLimit).Take(this.pagination.PageLimit);
            this.pagination.NumOfPageResult = this.pagination.Data.Count();
        }

        private void SetCurrentPage(int currentPage)
        {
            if(this.data == null || this.data.Count() == 0)
            {
                this.pagination.CurrentPage = 0;
                return;
            }

            if(this.pagination.PageLimit == 0)
            {
                this.pagination.PageLimit = 1;
            }
            var possibleNumOfPages = this.data.Count() / this.pagination.PageLimit;
            this.pagination.NumOfPages = possibleNumOfPages + 1;

            if(possibleNumOfPages < currentPage){
                currentPage = 0;
            }

            this.pagination.CurrentPage = currentPage;
        }

        private void SetLimit(int pageLimit)
        {
            this.pagination.PageLimit = pageLimit;
        }

        public Pagination<T> GetPagination()
        {
            return this.pagination;
        }

    }
}