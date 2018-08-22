using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.ViewModels
{
    //PaginatedItemsViewMode class is middle tier that allow u to render list of items 
    //from the API db on to  the UI
    //View (UI) <-> ViewModel<->Model(Domain/Data)

    public class PaginatedItemsViewModel<TEntity>
        where TEntity: class
    {
        public int PageSize { get; private set; }
        public int PageIndex { get; private set; }
        public long Count { get; private set; }
        public IEnumerable<TEntity> Data { get; set; }

        //constructor
        public PaginatedItemsViewModel(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
        {
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.Count = count;
            this.Data = data;
        }

    }
}
