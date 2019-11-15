using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.EF;
using PagedList;

namespace ShoeShop.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Metadata { get; set; }
        public List<string> ManufacturerList { get; set; }
        public List<string> StyleList { get; set; }
        public IPagedList<PRODUCT> ProductList { get; set; }
        /*-----------------------------------------------------------*/
        public string Brand { get; set; }
        public string Style { get; set; }
        public int? MinimumPrice { get; set; }
        public int? MaximumPrice { get; set; }
    }
}