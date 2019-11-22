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
        public PRODUCT GetProductById(int id)
        {
            return db.PRODUCT.Find(id);
        }

        public List<PRODUCT> GetAllProducts()
        {
            return db.PRODUCT.ToList();
        }

        public int InsertProduct(PRODUCT product)
        {
            db.PRODUCT.Add(product);
            db.SaveChanges();

            return product.Id;
        }

        public bool UpdateProduct(PRODUCT pd)
        {
            try
            {
                var updatedPd = db.PRODUCT.Find(pd.Id);

                updatedPd.Name = pd.Name;
                updatedPd.Price = pd.Price;
                updatedPd.Discount_Amount = pd.Discount_Amount;
                updatedPd.Image_Thumbnail = pd.Image_Thumbnail;
                updatedPd.Status = pd.Status;
                updatedPd.Model_Number = pd.Model_Number;
                updatedPd.Description = pd.Description;
                updatedPd.Style = pd.Style;
                updatedPd.Material = pd.Material;
                updatedPd.Warranty_Period = pd.Warranty_Period;
                updatedPd.String_Material = pd.String_Material;
                updatedPd.Id_Category = pd.Id_Category;
                updatedPd.Id_Manufacturer = pd.Id_Manufacturer;
                updatedPd.Metadata = pd.Metadata;

                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteProduct(int id)
        {
            using (var transactions = db.Database.BeginTransaction())
            {
                try
                {
                    var product = db.PRODUCT.Find(id);
                    db.PRODUCT.Remove(product);
                    db.SaveChanges();
                    transactions.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transactions.Rollback();
                    return false;
                }
            }
        }

        public List<PRODUCT> SearchProduct(string searchText)
        {
            return db.PRODUCT.Where(x => x.Name.Contains(searchText) || x.CATEGORY.Name.Contains(searchText)).ToList();
        }

        public List<PRODUCT> GetAllProduct()
        {
            return db.PRODUCT.OrderByDescending(n => n.Created_Time).ToList();
        }
        public void AddListImage(int id, string imgString)
        {
            var product_image = new PRODUCT_IMAGE
            {
                Id_Product = id,
                Data = imgString
            };
            db.PRODUCT_IMAGE.Add(product_image);
            db.SaveChanges();
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

        public int InsertProductImage(PRODUCT_IMAGE productImage)
        {
            db.PRODUCT_IMAGE.Add(productImage);
            db.SaveChanges();

            return productImage.Id;
        }

        public bool RemoveAllImagesByProductId(int id)
        {
            try
            {
                db.PRODUCT_IMAGE.RemoveRange(db.PRODUCT_IMAGE.Where(x => x.Id_Product == id));
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
    }
}
