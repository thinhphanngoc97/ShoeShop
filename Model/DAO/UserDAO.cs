using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.DAO
{
    public class UserDAO
    {
        ShoeShopDbContext db = null;
        
        public UserDAO()
        {
            db = new ShoeShopDbContext();
        }

		public List<USER> GetListUser()
		{
			return db.USER.OrderBy(x => x.Id).ToList();
		}

		public USER GetUser(int idUser)
		{
			return db.USER.Where(x => x.Id == idUser).SingleOrDefault();
		}

		public int InsertUser(USER user)
        {
			try
			{
				var result = db.USER.SingleOrDefault(x => x.Email == user.Email);

				if (result == null)
				{
					// Thêm tài khoản thành công
					user.Participation_Time = DateTime.Now;
					db.USER.Add(user);
					db.SaveChanges();
					return user.Id;
				}
				else
				{
					// Email trùng
					return 0;
				}
			}
			catch (Exception ex)
			{
				return -1;
			}
		}

		public int UpdateUser(USER entity)
		{
			try
			{
				var user = db.USER.Find(entity.Id);
				if (user.Email != entity.Email)
				{
					var result = db.USER.SingleOrDefault(x => x.Email == entity.Email);

					if (result == null)
					{
						user.Name = entity.Name;
						user.Phone = entity.Phone;
						user.Address = entity.Address;
						user.Email = entity.Email;
						user.Password = entity.Password;
						user.Avatar = entity.Avatar;
						db.SaveChanges();
						return user.Id;
					}
					else
					{
						//Username trùng
						return 0;
					}
				}

				user.Name = entity.Name;
				user.Phone = entity.Phone;
				user.Address = entity.Address;
				user.Password = entity.Password;
				user.Avatar = entity.Avatar;
				db.SaveChanges();
				return user.Id;
			}
			catch (Exception ex)
			{
				return -1;
			}
		}

		public bool DeleteUser(int id)
		{
			using (var transactions = db.Database.BeginTransaction())
			{
				try
				{
					var user = db.USER.Find(id);
					db.USER.Remove(user);
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

		public USER GetByEmail(string email)
        {
            return db.USER.SingleOrDefault(x => x.Email == email);			
        }

        public int LoginCheck(string email, string password)
        {
            var result = db.USER.SingleOrDefault(x => x.Email == email);
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
