using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoXamarin.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
		//Task InitAsync();
		Task<List<T>> GetAllAsync(bool forceRefresh = false);
    }
}