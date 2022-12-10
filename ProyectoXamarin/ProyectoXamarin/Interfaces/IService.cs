using System.Threading.Tasks;

namespace ProyectoXamarin.Interfaces
{
	public interface IService<T> where T : class
	{
		Task UpdateAsync(T entity);

		Task SaveAsync(T entity);
	}
}