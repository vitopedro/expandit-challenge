
export interface PaginatorDTO<T>{
    data:T[];
    numOfPages: number;
    numOfPageResult: number;
    numOfTotalResults: number;
    currentPage: number;
    pageLimit: number;
  }
  