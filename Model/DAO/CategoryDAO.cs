using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.DAO
{
    public class CategoryDAO
    {
        ShoeShopDbContext db = null;

        public CategoryDAO()
        {
            db = new ShoeShopDbContext();
        }

        public List<CATEGORY> GetListOfCategories()
        {
            return db.CATEGORY.OrderBy(x => x.Id).ToList();
        }

        public CATEGORY GetCategory(int id)
        {
            return db.CATEGORY.Where(x => x.Id == id).First();
        }
        public bool InsertCategory(CATEGORY entity)
        {
            try
            {
                entity.Metadata = entity.Name.ToLower().Replace(" ", "-");
                db.CATEGORY.Add(entity);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public CATEGORY UpdateCategory(CATEGORY entity)
        {
            try
            {
                var category = db.CATEGORY.Find(entity.Id);
                if (category.Name != entity.Name)
                {
                    category.Name = entity.Name;
                    category.Metadata = entity.Name.ToLower().Replace(" ", "-");
                    db.SaveChanges();
                }

                return category;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool DeleteCategory(int idCategory)
        {
            using (var transactions = db.Database.BeginTransaction())
            {
                try
                {
                    var category = db.CATEGORY.Find(idCategory);
                    db.CATEGORY.Remove(category);
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

        public List<string> GetListOfStylesAndQuantity()
        {
            List<string> styles = (from p in db.PRODUCT
                                   group p by new { p.Style } into g
                                   orderby g.Key.Style
                                   select g.Key.Style).ToList();
            styles.Insert(0, "Tất cả");
            return styles;
        }

        public List<string> GetListOfManufacturerNamesAndQuantity()
        {
            List<string> names = db.MANUFACTURER.OrderBy(x => x.Name).Select(x => x.Name).ToList();
            names.Insert(0, "Tất cả");
            return names;
        }

        public IPagedList<PRODUCT> GetProductList(int id, int? minimumPrice, int? maximumPrice, string brand, string style, int pageNumber = 1, int pageSize = 12)
        {
            var productList = db.PRODUCT.Where(x => x.Id_Category == id).OrderBy(x => x.Id).ToList();

            if (brand != "Tất cả" && brand != null)
            {
                productList = productList.Where(x => x.MANUFACTURER.Name == brand).OrderBy(x => x.Id).ToList();
            }

            if (style != "Tất cả" && style != null)
            {
                productList = productList.Where(x => x.Style == style).OrderBy(x => x.Id).ToList();
            }

            if (minimumPrice.HasValue)
            {
                productList = productList.Where(x => (x.Discount_Amount > 0 ? x.Price * (100 - x.Discount_Amount) / 100 : x.Price) >= minimumPrice).OrderBy(x => x.Id).ToList();
            }

            if (maximumPrice.HasValue)
            {
                productList = productList.Where(x => (x.Discount_Amount > 0 ? x.Price * (100 - x.Discount_Amount) / 100 : x.Price) <= maximumPrice).OrderBy(x => x.Id).ToList();
            }

            return productList.ToPagedList(pageNumber, pageSize);
        }
    }
}
