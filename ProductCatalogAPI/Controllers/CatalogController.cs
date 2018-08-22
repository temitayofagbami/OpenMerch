using System;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogAPI.Data;
using ProductCatalogAPI.Domain;
using Microsoft.Extensions.Options;
using ProductCatalogAPI.ViewModels;

namespace ProductCatalogAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Catalog")]
    public class CatalogController : Controller
    {
        //need access to the db in order to get/post items in the tables in it
        //so we get access to catalogcontext
        private readonly CatalogContext _catalogContext;
        
        //need to access json setting
        private readonly IOptionsSnapshot<CatalogSettings> _settings;

        //constructor to initialize catalong controller with current catalogcontext
        public CatalogController(CatalogContext catalogContext, IOptionsSnapshot<CatalogSettings> settings)
        {
            _catalogContext = catalogContext;
            _settings = settings;
        }

        //get all CatalogTypes
        [HttpGet]
        [Route("[action]")]  // the browser will replace [] with name of method, will be routed to the method - api/Catalog/CatalogTypes

        public async Task<IActionResult> CatalogTypes()
        {
            var items = await _catalogContext.CatalogTypes.ToListAsync();
            return Ok(items);
        }

        //get all CatalogBrands
        [HttpGet]
        [Route("[action]")]

        public async Task<IActionResult> CatalogBrands()
        {

            var items = await _catalogContext.CatalogBrands.ToListAsync();
            return Ok(items);
        }

        //get all CatalogItems and put 6 items on each page
        [HttpGet]
        [Route("[action]")]

        public async Task<IActionResult> Items(
            [FromQuery] int pageSize = 6,
            [FromQuery] int pageIndex = 0)

        {
            //get all the items count
            var totalItems = await _catalogContext.CatalogItems.LongCountAsync();

            //get the list of 6 items from CatalogItems table 
            //that should be rendered on each page
            var itemsOnPage = await _catalogContext.CatalogItems
                              .OrderBy(c => c.Name)
                              .Skip(pageSize * pageIndex)
                              .Take(pageSize)
                              .ToListAsync();

            //make pictures of 6 items ready to be rendered/retrieved with  current appsettings url
            itemsOnPage = ChangeUrlPlaceHolder(itemsOnPage);

            // put items into Paginated VM 
            var model = new PaginatedItemsViewModel<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage);
            return Ok(model);
                }


        //GetItemBy Id will return a unique Item
        [HttpGet]
        [Route("items/{id=int}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            //invalid id
            if(id <= 0)
            {
                BadRequest();
            }

            //find catalog item with specified id
            var item = await _catalogContext.CatalogItems.Where(c => c.Id == id).SingleOrDefaultAsync();

            //make picture of item ready to be rendered with current appsetting url
            if (item != null)
            {
                item.PictureUrl.Replace("http://externalcatalogbaseurltobereplaced",
                    _settings.Value.ExternalCatalogBaseUrl);

                return Ok(item);
            }
            //if item is null, item was not found
            return NotFound();

        }


        //get  CatalogItems by name and put 6 items on each page
        
        //for example: GET api/Catalog/items/withname/Wonder?pageSize=2&pageIndex=0

        [HttpGet]

        [Route("[action]/withname/{name:minlength(1)}")]

        public async Task<IActionResult> Items(string name,
            [FromQuery] int pageSize = 6,
            [FromQuery] int pageIndex = 0)
        {

            //get the items count for specific name
            var totalItems = await _catalogContext.CatalogItems
                               .Where(c => c.Name.StartsWith(name))
                              .LongCountAsync();

            //get the list of 6 items with specific name from CatalogItems table 
            //that should be rendered on each page
            var itemsOnPage = await _catalogContext.CatalogItems
                              .Where(c => c.Name.StartsWith(name))
                              .OrderBy(c => c.Name)
                              .Skip(pageSize * pageIndex)
                              .Take(pageSize)
                              .ToListAsync();
            //make pictures of 6 items ready to be rendered/retrieved with  current appsettings url
            itemsOnPage = ChangeUrlPlaceHolder(itemsOnPage);

            // put items into Paginated VM 
            var model = new PaginatedItemsViewModel<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage);
            return Ok(model);
        }

        //get items of specific Type and Brand
        // GET api/Catalog/Items/type/1/brand/null[?pageSize=4&pageIndex=0]

        [HttpGet]
        [Route("[action]/type/{catalogTypeId}/brand/{catalogBrandId}")]

        public async Task<IActionResult> Items(int? catalogTypeId,

            int? catalogBrandId,

            [FromQuery] int pageSize = 6,

            [FromQuery] int pageIndex = 0)

        {

            //get all items in catalogitem table and put in var root
            var root = (IQueryable<CatalogItem>)_catalogContext.CatalogItems;

            //select out catalog items of specified type id from root
            if (catalogTypeId.HasValue)
            {
                root = root.Where(c => c.CatalogTypeId == catalogTypeId);
            }

            // from what is selected  in root, select catalogitems
            if (catalogBrandId.HasValue)
            {
                root = root.Where(c => c.CatalogBrandId == catalogBrandId);
            }

            //get args for Paginated view model

            var totalItems = await root

                              .LongCountAsync();

            var itemsOnPage = await root
                              .OrderBy(c => c.Name)
                              .Skip(pageSize * pageIndex)

                              .Take(pageSize)

                              .ToListAsync();

            itemsOnPage = ChangeUrlPlaceHolder(itemsOnPage);

            var model = new PaginatedItemsViewModel<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);

        }

        //Command 

      //create item

        [HttpPost]
        [Route("items")]

        public async Task<IActionResult> CreateProduct([FromBody] CatalogItem product)
        {

            //call item construction
            var item = new CatalogItem

            {

                CatalogBrandId = product.CatalogBrandId,
                CatalogTypeId = product.CatalogTypeId,
                Description = product.Description,
                Name = product.Name,
               //PictureFileName = product.PictureFileName,
                Price = product.Price

            };

            //add item to catalog items table
            _catalogContext.CatalogItems.Add(item);

            //save changes
            await _catalogContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItemById), new { id = item.Id });
        }




        //update item
        [HttpPut]
        [Route("items")]

        public async Task<IActionResult> UpdateProduct(
            [FromBody] CatalogItem productToUpdate)
        {
            //find updated item's id in table
            var catalogItem = await _catalogContext.CatalogItems.SingleOrDefaultAsync(i => i.Id == productToUpdate.Id);

            //if it does not exist return not found
            if (catalogItem == null)
            {
                return NotFound(new { Message = $"Item with id {productToUpdate.Id} not found." });
            }

            //update the old item from table with updated item 
            catalogItem = productToUpdate;

            //update it in table
            _catalogContext.CatalogItems.Update(catalogItem);

            //save changes
            await _catalogContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItemById), new { id = productToUpdate.Id });

        }

        //delete item
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)

        {
            //find item to be deleted in table
            var product = await _catalogContext.CatalogItems.SingleOrDefaultAsync(p => p.Id == id);

            //if it does not exist return not found
            if (product == null)
            {
                return NotFound();
            }

            //remove product
            _catalogContext.CatalogItems.Remove(product);

            //commit changes
            await _catalogContext.SaveChangesAsync();

            return NoContent();
        }



        private List<CatalogItem> ChangeUrlPlaceHolder(List<CatalogItem> items)
        {
          
            //need to create catalog setting to read externalcatalogbase url
            //from appsetting json
              items.ForEach(
                   x => x.PictureUrl =
                   x.PictureUrl
                   .Replace("http://externalcatalogbaseurltobereplaced",
                   _settings.Value.ExternalCatalogBaseUrl));
                   

            return items;

        }


    }

}  