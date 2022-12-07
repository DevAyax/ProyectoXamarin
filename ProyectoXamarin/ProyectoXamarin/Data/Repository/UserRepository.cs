using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoXamarin.Data.Repository;
using ProyectoXamarin.Models.Users;
using Xamarin.Forms;

[assembly: Dependency(typeof(UserRepository))]

namespace ProyectoXamarin.Data.Repository
{
	/// <summary>
	/// Defines the <see cref="IUserRepository" />.
	/// </summary>
	public class UserRepository : IUserRepository
	{
		public UserRepository()
		{
		}

		public async Task<User> GetByIdAsync(int id)
		{
			var User = await App.DataBase.db.Table<User>().Where(b => b.Id == id).FirstOrDefaultAsync();
			await App.DataBase.db.CloseAsync();
			return User;
		}

		public async Task<List<User>> GetAllAsync(bool forceRefresh = false)
		{
			var users = await App.DataBase.db.Table<User>().ToListAsync();
			var user = users.OrderByDescending(c => c.Id).FirstOrDefault();
			return users;
		}

		public async Task<int> DeleteItemAsync(int id)
		{
			var User = await App.DataBase.db.Table<User>().Where(b => b.Id == id).FirstOrDefaultAsync();
			return await App.DataBase.db.DeleteAsync(User);
		}

		public async Task<int> SaveItemAsync(User user)
		{
			var status = await App.DataBase.db.InsertAsync(user);

			if (status == 1)
				await UpdateSesionDataUser(status, user);

			return status;
		}

		public async Task UpdateSesionDataUser(int status, User user)
		{
			var newUser = await App.DataBase.db.Table<User>().Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefaultAsync();
			SesionData.UserId = newUser.Id;
		}

		public async Task<int> UpdateItemAsync(User item)
		{
			var status = await App.DataBase.db.UpdateAsync(item);

			await UpdateSesionDataUser(status, item);

			status = status == 1 ? status++ : status;

			return status;
		}
	}
}