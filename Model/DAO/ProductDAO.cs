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

        public PRODUCT GetProductById(int id)
        {
            return db.PRODUCT.Find(id);
        }

        public void GetProductExtraInfo(int id)
        {
            // Lấy list hình ảnh của sản phẩm
            ListImage = db.PRODUCT_IMAGE.Where(x => x.Id_Product == id).ToList();

            // Lấy list sản phẩm liên quan
            var product = GetProductById(id);
            ListRelatedProducts = db.PRODUCT.Where(x => x.Id != id && x.Id_Category == product.Id_Category).OrderByDescending(x => x.Discount_Amount).Take(8).ToList();

            // Đếm tổng số lượt xếp hạng sản phẩm
            var listRate = db.RATE.Where(x => x.Id_Product == id);
            TotalRate = listRate.Count();

            // Đếm số lượt của mỗi bậc xếp hạng
            ListCountRate = new int[5];
            for (int i = 0; i < 5; i++)
            {
                ListCountRate[i] = db.RATE.Where(x => x.Id_Product == id && x.Number_Of_Stars == i + 1).Count();
            }

            // Tính xếp hạng trung bình
            if (TotalRate > 0)
            {
                AverageRate = listRate.Average(x => x.Number_Of_Stars);
            }
            else
            {
                AverageRate = 0;
            }
        }

        public void SaveRating(double number_of_stars, int product_id)
        {
            RATE r = new RATE { Number_Of_Stars = number_of_stars, Id_Product = product_id };
            db.RATE.Add(r);
            db.SaveChanges();

            // Tính lại các biến xếp hạng
            // Đếm tổng số lượt xếp hạng sản phẩm
            var listRate = db.RATE.Where(x => x.Id_Product == product_id);
            TotalRate = listRate.Count();

            // Đếm số lượt của mỗi bậc xếp hạng
            ListCountRate = new int[5];
            for (int i = 0; i < 5; i++)
            {
                ListCountRate[i] = db.RATE.Where(x => x.Id_Product == product_id && x.Number_Of_Stars == i + 1).Count();
            }

            // Tính xếp hạng trung bình
            if (TotalRate > 0)
            {
                AverageRate = listRate.Average(x => x.Number_Of_Stars);
            }
            else
            {
                AverageRate = 0;
            }
        }
    }
}
