using System.Threading.Tasks;
using ProyectoXamarin.Interfaces;

namespace ProyectoXamarin.Models.Brands
{
	public interface IBrandRepository : IRepository<Brand>
	{
		Task SetBrandsIntoDataBase();
	}
}