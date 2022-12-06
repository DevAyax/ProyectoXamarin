using System.Threading.Tasks;

namespace ProyectoXamarin.Interfaces
{
	public interface IService<T> where T : class
	{
		//Task InitAsync();

		Task SaveAsync(T entity);
	}
}
