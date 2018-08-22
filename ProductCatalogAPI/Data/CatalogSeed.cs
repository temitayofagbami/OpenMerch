
using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Data
{

    public class CatalogSeed

    {
        //add/seeds dummy data into tables
        public static async Task SeedAsync(CatalogContext context)

        {
            //check to see if the snapshot of my database uptodate
            //does the schema of tables in db in my server match up with my class definitions
            context.Database.Migrate();

            //check first if CatalogCategories does not have any categories in it
            if (!context.CatalogBrands.Any())

            {
                //add/seed dummy brands into CatalogBrand table
                context.CatalogCategories.AddRange(GetPreconfiguredCatalogCategories());
                //commit
                await context.SaveChangesAsync();

            }

            //check first if CatalogBrands does not have any brands in it
            if (!context.CatalogBrands.Any())

            {
                //add/seed dummy brands into CatalogBrand table
                context.CatalogBrands.AddRange(GetPreconfiguredCatalogBrands());
                //commit
                await context.SaveChangesAsync();

            }

            if (!context.CatalogTypes.Any())
            {
                //add/seed dummy types into CatalogTypes table
                context.CatalogTypes.AddRange(GetPreconfiguredCatalogTypes());
                //commit
                context.SaveChanges();

            }

            if (!context.CatalogItems.Any())
            {
                //add/seed dummy items into CatalogItems table
                context.CatalogItems.AddRange(GetPreconfiguredItems());
                //commit
                context.SaveChanges();

            }



        }



        static IEnumerable<CatalogCategory> GetPreconfiguredCatalogCategories()

        {

            return new List<CatalogCategory>()

            {

                new CatalogCategory() { Category = "Shoes"},

                new CatalogCategory() { Category = "Apparel"},

                new CatalogCategory() { Category = "Accessory"},

                new CatalogCategory() { Category = "Jewellery"}


            };

        }


        static IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()

        {

            return new List<CatalogBrand>()

            {

                new CatalogBrand() { Brand = "Addidas"},

                new CatalogBrand() { Brand = "Puma" },

                new CatalogBrand() { Brand = "Slazenger" },

                new CatalogBrand() { Brand = "Calvin Klein"},

                new CatalogBrand() { Brand = "Versace"},

                new CatalogBrand() { Brand = "Tommy Hilfiger"},

                new CatalogBrand() { Brand = "Tiffanys"},

                new CatalogBrand() { Brand = "Pandora"},

            };

        }



        static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()

        {

            return new List<CatalogType>()

            {

                new CatalogType() { Type = "Running"},

                new CatalogType() { Type = "Boots" },

                new CatalogType() { Type = "Pumps" },

                new CatalogType() { Type = "Shirt"},

                new CatalogType() { Type = "Dress" },

                new CatalogType() { Type = "Pants" },

               new CatalogType() { Type = "Earrings"},

                new CatalogType() { Type = "Bracelet" },

                new CatalogType() { Type = "Necklace" },

            };

        }



        static IEnumerable<CatalogItem> GetPreconfiguredItems()

        {

            return new List<CatalogItem>()

            {

                new CatalogItem() { CatalogCategoryId=1, CatalogTypeId=2,CatalogBrandId=3, Description = "Shoes for next century", Name = "World Star", Price = 199.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/1" },

                new CatalogItem() {  CatalogCategoryId=2, CatalogTypeId=1,CatalogBrandId=2, Description = "will make you world champions", Name = "White Line", Price= 88.50M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/2" },

                new CatalogItem() {  CatalogCategoryId=3, CatalogTypeId=2,CatalogBrandId=3, Description = "You have already won gold medal", Name = "Prism White Shoes", Price = 129, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/3" },

                new CatalogItem() {  CatalogCategoryId=2, CatalogTypeId=2,CatalogBrandId=2, Description = "Olympic runner", Name = "Foundation Hitech", Price = 12, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/4" },

                new CatalogItem() { CatalogCategoryId=4, CatalogTypeId=2,CatalogBrandId=1, Description = "Roslyn Red Sheet", Name = "Roslyn White", Price = 188.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/5" },

                new CatalogItem() {  CatalogCategoryId=2, CatalogTypeId=2,CatalogBrandId=2, Description = "Lala Land", Name = "Blue Star", Price = 112, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/6" },

                new CatalogItem() {  CatalogCategoryId=1, CatalogTypeId=2,CatalogBrandId=1, Description = "High in the sky", Name = "Roslyn Green", Price = 212, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/7"  },

                new CatalogItem() {  CatalogCategoryId=2, CatalogTypeId=1,CatalogBrandId=1, Description = "Light as carbon", Name = "Deep Purple", Price = 238.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/8" },

                new CatalogItem() {  CatalogCategoryId=2, CatalogTypeId=1,CatalogBrandId=2, Description = "High Jumper", Name = "Addidas<White> Running", Price = 129, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/9" },

                new CatalogItem() {  CatalogCategoryId=4, CatalogTypeId=2,CatalogBrandId=3, Description = "Dunker", Name = "Elequent", Price = 12, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/10" },

                new CatalogItem() {  CatalogCategoryId=2, CatalogTypeId=1,CatalogBrandId=2, Description = "All round", Name = "Inredeible", Price = 248.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/11" },

                new CatalogItem() {  CatalogCategoryId=3, CatalogTypeId=2,CatalogBrandId=1, Description = "Pricesless", Name = "London Sky", Price = 412, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/12" },

                new CatalogItem() {  CatalogCategoryId=2, CatalogTypeId=3,CatalogBrandId=3, Description = "Tennis Star", Name = "Elequent", Price = 123, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/13" },

                new CatalogItem() {  CatalogCategoryId=1, CatalogTypeId=3,CatalogBrandId=2, Description = "Wimbeldon", Name = "London Star", Price = 218.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/14" },

                new CatalogItem() {  CatalogCategoryId=4, CatalogTypeId=3,CatalogBrandId=1, Description = "Rolan Garros", Name = "Paris Blues", Price = 312, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/15" }



            };

        }

    }

}