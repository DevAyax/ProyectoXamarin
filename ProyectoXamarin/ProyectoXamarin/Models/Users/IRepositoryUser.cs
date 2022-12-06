using System.Threading.Tasks;
using ProyectoXamarin.Interfaces;
using ProyectoXamarin.Models.Cars;

namespace ProyectoXamarin.Models.Users
{
	public interface IUserRepository : IRepository<User>
	{
		Task<int> DeleteItemAsync(int id);
		Task<int> SaveItemAsync(User item);
		Task<int> UpdateItemAsync(User item);
		Task AddConstantUser(int status, User user);
	}
}
