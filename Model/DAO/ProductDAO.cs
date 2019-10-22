using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ProductDAO
    {
        ShoeShopDbContext db = null;

        public List<PRODUCT_IMAGE> ListImage { get; set; }
        public List<PRODUCT_IMAGE> _ListImage { get; set; }
        public List<PRODUCT> ListRelatedProducts { get; set; }
        public int TotalRate { get; set; }
        public double? AverageRate { get; set; }
        public int[] ListCountRate { get; set; }

        public ProductDAO()
        {
            db = new ShoeShopDbContext();
        }

        public List<PRODUCT> GetMostDiscountProducts()
        {
            return db.PRODUCT.OrderByDescending(x => x.Discount_Amount).Take(8).ToList();
        }

        public List<PRODUCT> GetNewestProducts()
        {
            return db.PRODUCT.OrderByDescending(x => x.Created_Time).Take(8).ToList();
        }
    }
}
