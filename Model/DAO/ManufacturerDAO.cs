using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ManufacturerDAO
    {
        ShoeShopDbContext db = null;
        public ManufacturerDAO()
        {
            db = new ShoeShopDbContext();
        }

        public List<MANUFACTURER> GetAllManufacturers()
        {
            return db.MANUFACTURER.ToList();
        }
       
        public MANUFACTURER GetManufacturerById(int id)
        {
            return db.MANUFACTURER.Where(x => x.Id == id).SingleOrDefault();
        }

        public int Insert(MANUFACTURER manufacturer)
        {
            db.MANUFACTURER.Add(manufacturer);
            db.SaveChanges();
            return manufacturer.Id;
        }
        
        public bool Update(MANUFACTURER manufacturer)
        {
            try
            {
                var manu = db.MANUFACTURER.Find(manufacturer.Id);
                manu.Name = manufacturer.Name;
                manu.Country = manufacturer.Country;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool DeleteManufacturer(int id)
        {
            using (var transactions = db.Database.BeginTransaction())
            {
                try
                {
                    var manufacturer = db.MANUFACTURER.Find(id);
                    db.MANUFACTURER.Remove(manufacturer);
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
