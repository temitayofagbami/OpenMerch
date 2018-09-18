using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Services;
using WebMvc.ViewModels;


namespace WebMvc.Controllers
{
    public class ProductDetailController : Controller
    {
        //create property catalogservice 
        private ICatalogService _catalogSvc;

        //constructor to initialize catalogsvc from setting from startup

        public ProductDetailController(ICatalogService catalogSvc) =>

       _catalogSvc = catalogSvc;


        public async Task<IActionResult> Index(int id)
        {
            //make aservice call to get a single catalog item by  id
            //service call gets api url
            //then makes api call and return jsonrespons package as a catalogItem is made which call methis GetItemById(intid)

          var pdetails = await _catalogSvc.GetCatalogItem(id);

            //put in viewm model to be presented
            var vm = new ProductDetailViewModel()
            {



                Id = pdetails.Id,
                Name = pdetails.Name,
                Description = pdetails.Description,
                Price = pdetails.Price,
                PictureUrl = pdetails.PictureUrl,
                CatalogBrandId = pdetails.CatalogBrandId,
                CatalogBrand = pdetails.CatalogBrand,
                CatalogTypeId = pdetails.CatalogTypeId,
                CatalogType = pdetails.CatalogType,
                CatalogCategoryId = pdetails.CatalogCategoryId,
                CatalogCategory = pdetails.CatalogCategory
            };
            
            return View(vm);
        }
    }
}
