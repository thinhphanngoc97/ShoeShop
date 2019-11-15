using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
	public class AdminDAO
	{
		ShoeShopDbContext db = null;

		public AdminDAO()
		{
			db = new ShoeShopDbContext();
		}

		public List<ADMIN> GetListAdmin()
		{
			return db.ADMIN.OrderBy(x => x.Id).ToList();
		}

		public ADMIN GetAdmin(int idAdmin)
		{
			return db.ADMIN.Where(x => x.Id == idAdmin).SingleOrDefault();
		}

		public ADMIN GetAdmin(string username)
		{
			return db.ADMIN.Where(x => x.Username == username).SingleOrDefault();
		}

		public int InsertAdmin(ADMIN admin)
		{
			try
			{
				var result = db.ADMIN.SingleOrDefault(x => x.Username == admin.Username);

				if (result == null)
				{
					// Thêm tài khoản thành công
					db.ADMIN.Add(admin);
					db.SaveChanges();
					return admin.Id;
				}
				else
				{
					// Username trùng
					return 0;
				}
			}
			catch (Exception ex)
			{
				return -1;
			}
		}

		public int UpdateAdmin(ADMIN entity)
		{
			try
			{
				var admin = db.ADMIN.Find(entity.Id);
				if(admin.Username != entity.Username)
				{
					var result = db.ADMIN.SingleOrDefault(x => x.Username == entity.Username);

					if (result == null)
					{
						admin.Name = entity.Name;
						admin.Username = entity.Username;
						admin.Password = entity.Password;
						admin.Avatar = entity.Avatar;
						db.SaveChanges();
						return admin.Id;
					}
					else
					{
						//Username trùng
						return 0;
					}
				}

				admin.Username = entity.Username;
				admin.Password = entity.Password;
				admin.Avatar = entity.Avatar;
				db.SaveChanges();
				return admin.Id;
			}
			catch (Exception ex)
			{
				return -1;
			}
		}

		public int DeleteAdmin(int id, ADMIN entity)
		{
			if (entity.Id == id)
				return 0;

			using (var transactions = db.Database.BeginTransaction())
			{
				try
				{
					var admin = db.ADMIN.Find(id);
					db.ADMIN.Remove(admin);
					db.SaveChanges();
					transactions.Commit();

					return 1;
				}
				catch (Exception ex)
				{
					transactions.Rollback();
					return -1;
				}
			}
		}

		public int LoginCheck(string username, string password)
		{
			var result = db.ADMIN.SingleOrDefault(x => x.Username == username);
			if (result == null)
			{
				// Không tồn tại tài khoản
				return -1;
			}
			else
			{
				if (result.Password == password)
				{
					// Tài khoản và mật khẩu trùng khớp
					return 1;
				}
				else
				{
					// Mật khẩu không đúng
					return 0;
				}
			}
		}
	}
}
