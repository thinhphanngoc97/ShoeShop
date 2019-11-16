using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class InvoiceDetailDAO
    {
        ShoeShopDbContext db = null;

        public InvoiceDetailDAO()
        {
            db = new ShoeShopDbContext();
        }

        public bool Insert(INVOICE_DETAIL invoiceDetail)
        {
            try
            {
                db.INVOICE_DETAIL.Add(invoiceDetail);
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
