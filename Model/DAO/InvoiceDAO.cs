using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class InvoiceDAO
    {
        GuitarShopDbContext db = null;

        public InvoiceDAO()
        {
            db = new GuitarShopDbContext();
        }

        public int Insert(INVOICE invoice)
        {
            db.INVOICE.Add(invoice);
            db.SaveChanges();
            return invoice.Id;
        }

		public INVOICE Update(INVOICE entity)
		{
			try
			{
				var invoice = db.INVOICE.Find(entity.Id);
				invoice.Customer_Name = entity.Customer_Name;
				invoice.Customer_Phone = entity.Customer_Phone;
				invoice.Customer_Email = entity.Customer_Email;
				invoice.Customer_Address = entity.Customer_Address;
				invoice.Customer_Message = entity.Customer_Message;
				invoice.Status = entity.Status;
				invoice.Payment_Method = entity.Payment_Method;
				db.SaveChanges();

				return invoice;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public int GetNumberInvoice()
		{
			return db.INVOICE.Where(x => x.Status == 0).Count();
		}

		public INVOICE GetInvoice(int idInvoice)
		{
			return db.INVOICE.Where(x => x.Id == idInvoice).SingleOrDefault();
		}

		public List<INVOICE> GetListInvoice(int idProperty, int idTime)
		{
			var listInvoice = db.INVOICE.OrderBy(x => x.Id).ToList();
			
			//Lọc theo trạng thai đơn hàng
			switch (idProperty)
			{
				case 1:
					listInvoice = listInvoice.Where(x => x.Status == 0).OrderBy(x => x.Id).ToList();
					break;
				case 2:
					listInvoice = listInvoice.Where(x => x.Status == 1).OrderBy(x => x.Id).ToList();
					break;
				default:
					break;
			}

			//Lọc theo thời gian đơn hàng
			switch (idTime)
			{
				case 1:
					listInvoice = listInvoice.Where(x => x.Created_Time.Value.Date == DateTime.Now.Date).OrderBy(x => x.Id).ToList();
					break;
				case 2:
					listInvoice = listInvoice.Where(x => x.Created_Time.Value.Month == DateTime.Now.Month).OrderBy(x => x.Id).ToList();
					break;
				default:
					break;
			}

			return listInvoice;			
		}

		public INVOICE updateInvoiceStatus(int id)
		{
			var invoice = db.INVOICE.Where(x => x.Id == id).SingleOrDefault();
			invoice.Status = invoice.Status == 0? 1 : 0;
			db.SaveChanges();

			return invoice;
		}

	}
}
