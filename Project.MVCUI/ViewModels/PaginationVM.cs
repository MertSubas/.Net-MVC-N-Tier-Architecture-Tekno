using PagedList;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.ViewModels
{
    public class PaginationVM
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
        public IPagedList PagedProducts { get; set; }

    }
}