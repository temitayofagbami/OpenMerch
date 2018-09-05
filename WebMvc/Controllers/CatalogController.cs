﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Services;
using WebMvc.ViewModels;

namespace WebMvc.Controllers
{
    public class CatalogController: Controller
    {
        private ICatalogService _catalogSvc;



        public CatalogController(ICatalogService catalogSvc) =>

            _catalogSvc = catalogSvc;



        public async Task<IActionResult> Index(

            int? BrandFilterApplied,

            int? TypeFilterApplied, int? page, int? CategoryFilterApplied)

        {



            int itemsPage = 10;

            var catalog = await

                _catalogSvc.GetCatalogItems

                (page ?? 0, itemsPage, BrandFilterApplied,

                TypeFilterApplied, CategoryFilterApplied);

            var vm = new CatalogIndexViewModel()

            {

                CatalogItems = catalog.Data,

                Brands = await _catalogSvc.GetBrands(),

                Types = await _catalogSvc.GetTypes(),

                Categories = await _catalogSvc.GetCategories(),

                BrandFilterApplied = BrandFilterApplied ?? 0,

                TypeFilterApplied = TypeFilterApplied ?? 0,

                CategoryFilterApplied = CategoryFilterApplied ?? 0,

                PaginationInfo = new PaginationInfo()

                {

                    ActualPage = page ?? 0,

                    ItemsPerPage = itemsPage, //catalog.Data.Count,

                    TotalItems = catalog.Count,

                    TotalPages = (int)Math.Ceiling(((decimal)catalog.Count / itemsPage))

                }

            };



            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";

            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";
            
            return View(vm);

        }
    }
}
