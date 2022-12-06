using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoXamarin.Models.Users;

namespace ProyectoXamarin.Interfaces
{
    public interface IUserService : IService<User>
    {
		Task UpdateAsync(User entity);
		Task<bool> ExistAsync(User entity);
		Task<List<User>> GetAllUsersAsync();
		Task<User> GetUserAsync(int id);

	}
}