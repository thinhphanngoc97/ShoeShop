using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class MessageDAO
    {
        ShoeShopDbContext db = null;

        public MessageDAO()
        {
            db = new ShoeShopDbContext();
        }

        public MESSAGE GetMessageId(int id)
        {
            return db.MESSAGE.Where(x => x.Id == id).SingleOrDefault();
        }
        public List<MESSAGE> GetAllMessage()
        {
            return db.MESSAGE.ToList();
        }
        public bool DeleteMessage(int id)
        {
            using (var transactions = db.Database.BeginTransaction())
            {
                try
                {
                    var message = db.MESSAGE.Find(id);
                    db.MESSAGE.Remove(message);
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
