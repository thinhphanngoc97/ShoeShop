using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ContactDAO
    {
        ShoeShopDbContext db = null;

        public ContactDAO()
        {
            db = new ShoeShopDbContext();
        }
        public int Insert(MESSAGE contact)
        {
            db.MESSAGE.Add(contact);
            db.SaveChanges();
            return contact.Id;
        }
    }
}
