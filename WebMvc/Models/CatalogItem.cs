using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    //create a model in webmvc based on the model of the api/table related to model
    //the return types of the model of class in mvc has to match the types
    //in the table related to model (CatalogItems table)
    //remeber this webmvc class will take data from CatalogItems table through its Catalog API 
    //and rendered it on UI so it types has to mirror record is stored in table
    //also we dont have to mirror all data , only the ones we wish to render on UI
    //so basically when httpclient makes request (like HTTPGET) to get a catalog item from catalog table
    //instead of returning a json, it maps the data from the table  to an object of this class
    //ready to be sent as response to httpclient.
    public class CatalogItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int CatalogBrandId { get; set; }
        public string CatalogBrand { get; set; }
        public int CatalogTypeId { get; set; }
        public string CatalogType { get; set; }
    }
}
