﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvc.ViewModels
{
    public class CatalogIndexViewModel
    {
        public IEnumerable<CatalogItem> CatalogItems { get; set; }

        public IEnumerable<SelectListItem> Brands { get; set; }

        public IEnumerable<SelectListItem> Types { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public int? BrandFilterApplied { get; set; }

        public int? TypeFilterApplied { get; set; }

        public int? CategoryFilterApplied { get; set; }

        public PaginationInfo PaginationInfo { get; set; }
    }
}
