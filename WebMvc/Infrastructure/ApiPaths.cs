using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Infrastructure
{
    public class ApiPaths
    {
        public static class Catalog

        {

            public static string GetAllCatalogItems(string baseUri,

                int page, int take, int? brand, int? type, int? category)

            {

                var filterQs = string.Empty;



                if (brand.HasValue || type.HasValue || category.HasValue)

                {

                    var brandQs = (brand.HasValue) ? brand.Value.ToString() : "null";

                    var typeQs = (type.HasValue) ? type.Value.ToString() : "null";

                    var categoryQs = (category.HasValue) ? category.Value.ToString() : "null";

                    filterQs = $"/type/{typeQs}/brand/{brandQs}/category/{categoryQs}";

                }



                //return $"{baseUri}items{filterQs}?pageSize={take}&pageindex={page}";
                return $"{baseUri}items";
            }



            public static string GetCatalogItem(string baseUri, int id)

            {



                return $"{baseUri}/items/{id}";

            }

            public static string GetAllBrands(string baseUri)

            {

                return $"{baseUri}catalogBrands";

            }

            public static string GetAllCategories(string baseUri)

            {

                return $"{baseUri}catalogCategories";

            }



            public static string GetAllTypes(string baseUri)

            {

                return $"{baseUri}catalogTypes";

            }

        }
    }
}
