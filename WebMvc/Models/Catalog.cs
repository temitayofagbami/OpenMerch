using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    // gets a list of each catalog items already mapped to webmvc catalogitem 
    //and render it to UI in a list with pagesize and index

    public class Catalog
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }

        public List<CatalogItem> Data { get; set; }
    }
}
