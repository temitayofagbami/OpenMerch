using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebMvc.Infrastructure;
using WebMvc.Models;
using WebMvc.Services;

namespace WebMvc.Services
{
    public class CatalogService: ICatalogService
    {
        private readonly IOptionsSnapshot<AppSettings> _settings;

        private readonly IHttpClient _apiClient;

        private readonly string _remoteServiceBaseUrl;

        public CatalogService(IOptionsSnapshot<AppSettings> settings,

            IHttpClient httpClient)

        {

            _settings = settings;

            _apiClient = httpClient;

            _remoteServiceBaseUrl = $"{_settings.Value.CatalogUrl}/api/catalog/";



        }



        public async Task<IEnumerable<SelectListItem>> GetBrands()

        {

            var getBrandsUri = ApiPaths.Catalog.GetAllBrands(_remoteServiceBaseUrl);



            var dataString = await _apiClient.GetStringAsync(getBrandsUri);



            var items = new List<SelectListItem>

            {

                new SelectListItem() { Value = null, Text = "All", Selected = true }

            };

            var brands = JArray.Parse(dataString);



            foreach (var brand in brands.Children<JObject>())

            {

                items.Add(new SelectListItem()

                {

                    Value = brand.Value<string>("id"),

                    Text = brand.Value<string>("brand")

                });

            }



            return items;

        }



        public async Task<IEnumerable<SelectListItem>> GetCategories()

        {

            var getBrandsUri = ApiPaths.Catalog.GetAllCategories(_remoteServiceBaseUrl);



            var dataString = await _apiClient.GetStringAsync(getBrandsUri);



            var items = new List<SelectListItem>

            {

                new SelectListItem() { Value = null, Text = "All", Selected = true }

            };

            var categories = JArray.Parse(dataString);



            foreach (var category in categories.Children<JObject>())

            {

                items.Add(new SelectListItem()

                {

                    Value = category.Value<string>("id"),

                    Text = category.Value<string>("category")

                });

            }



            return items;

        }






        public async Task<IEnumerable<SelectListItem>> GetTypes()

        {

            var getTypesUri = ApiPaths.Catalog.GetAllTypes(_remoteServiceBaseUrl);



            var dataString = await _apiClient.GetStringAsync(getTypesUri);



            var items = new List<SelectListItem>

            {

                new SelectListItem() { Value = null, Text = "All", Selected = true }

            };

            var brands = JArray.Parse(dataString);

            foreach (var brand in brands.Children<JObject>())

            {

                items.Add(new SelectListItem()

                {

                    Value = brand.Value<string>("id"),

                    Text = brand.Value<string>("type")

                });

            }

            return items;

        }


        //get all items
        public async Task<Catalog> GetCatalogItems(int page, int take, int? brand, int? type, int? category)

        {

            var allcatalogItemsUri = ApiPaths.Catalog.GetAllCatalogItems(_remoteServiceBaseUrl, page, take, brand, type, category);



            var dataString = await _apiClient.GetStringAsync(allcatalogItemsUri);



            var response = JsonConvert.DeserializeObject<Catalog>(dataString);



            return response;

        }

        //get an item by id

            //service that gets an item by id
        public async Task<CatalogItem> GetCatalogItem(int id)
        {
            //get api url
            var allcatalogItemsUri = ApiPaths.Catalog.GetCatalogItem(_remoteServiceBaseUrl, id);

            //make api call and return data as string

            var dataString = await _apiClient.GetStringAsync(allcatalogItemsUri);

            //parse the datastring into a catalogItem object to be passed to view for presentation
            //??ask about wording

            var response = JsonConvert.DeserializeObject<CatalogItem>(dataString);



            return response;

        }

    }
}
