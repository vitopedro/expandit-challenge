using System.Collections.Generic;

namespace Challenge.Helpers.Pagination
{
    public class Pagination<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int NumOfPages { get; set; }
        public int NumOfPageResult { get; set; }
        public int NumOfTotalResults { get; set; }
        public int CurrentPage { get; set; }
        public int PageLimit { get; set; }
    }
}