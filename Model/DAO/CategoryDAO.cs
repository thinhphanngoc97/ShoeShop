using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
