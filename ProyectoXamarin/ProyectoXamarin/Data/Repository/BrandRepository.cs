using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoXamarin.Data.Repository;
using ProyectoXamarin.Models.Brands;
using Xamarin.Forms;

[assembly: Dependency(typeof(BrandRepository))]

namespace ProyectoXamarin.Data.Repository
{
	public class BrandRepository : IBrandRepository
	{
		public BrandRepository()
		{
		}

		public async Task<Brand> GetByIdAsync(int id)
		{
			var brand = await App.DataBase.db.Table<Brand>().Where(b => b.Id == id).FirstOrDefaultAsync();
			await App.DataBase.db.CloseAsync();
			return brand;
		}

		public async Task<List<Brand>> GetAllAsync(bool forceRefresh = false)
		{
			var brands = await App.DataBase.db.Table<Brand>().OrderBy(b => b.Name).ToListAsync();
			return brands;
		}
	}
}