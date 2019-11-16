using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class DiscountCodeDAO
    {
        ShoeShopDbContext db = null;

        public DiscountCodeDAO()
        {
            db = new ShoeShopDbContext();
        }

        public DISCOUNT_CODE GetDiscountCodeByCode(string code)
        {
            var now = DateTime.Now.Date;
            return db.DISCOUNT_CODE.Where(x => x.Code == code && x.Start_Date <= now && x.End_Date >= now).SingleOrDefault();
        }
        public DISCOUNT_CODE GetDiscountCodeById(int id)
        {
            return db.DISCOUNT_CODE.Find(id);
        }
        public List<DISCOUNT_CODE> GetAllDiscountCode()
        {
            return db.DISCOUNT_CODE.OrderBy(n => n.Id).ToList();
        }
        public int Insert(DISCOUNT_CODE discount)
        {
            try
            {
                var result = db.DISCOUNT_CODE.SingleOrDefault(x => x.Code == discount.Code);

                if (result == null)
                {
                    db.DISCOUNT_CODE.Add(discount);
                    db.SaveChanges();
                    return discount.Id;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public int Update(DISCOUNT_CODE discount)
        {
            try
            {
                var discount_code = db.DISCOUNT_CODE.Find(discount.Id);
                if (discount_code.Code != discount.Code)
                {
                    var result = db.DISCOUNT_CODE.SingleOrDefault(x => x.Code == discount.Code);

                    if (result == null)
                    {
                        discount_code.Code = discount.Code;
                        discount_code.Discount_Amount = discount.Discount_Amount;
                        discount_code.Start_Date = discount.Start_Date;
                        discount_code.End_Date = discount.End_Date;
                        db.SaveChanges();
                        return discount_code.Id;
                    }
                    else
                    {
                        return 0;
                    }
                }

                discount_code.Code = discount.Code;
                discount_code.Discount_Amount = discount.Discount_Amount;
                discount_code.Start_Date = discount.Start_Date;
                discount_code.End_Date = discount.End_Date;
                db.SaveChanges();
                return discount_code.Id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public bool DeleteDiscount(int id)
        {
            using (var transactions = db.Database.BeginTransaction())
            {
                try
                {
                    var discount = db.DISCOUNT_CODE.Find(id);
                    db.DISCOUNT_CODE.Remove(discount);
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
    }
}
