using System.Threading.Tasks;
using ProyectoXamarin.Interfaces;

namespace ProyectoXamarin.Models.Users
{
	public interface IUserRepository : IRepository<User>
	{
		Task<int> DeleteItemAsync(int id);

		Task<int> SaveItemAsync(User item);

		Task<int> UpdateItemAsync(User item);

		Task UpdateSesionDataUser(int status, User user);
	}
}