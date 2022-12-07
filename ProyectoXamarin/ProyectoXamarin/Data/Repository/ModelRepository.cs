using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoXamarin.Data.Repository;
using ProyectoXamarin.Models.ModelCar;
using Xamarin.Forms;

[assembly: Dependency(typeof(ModelRepository))]

namespace ProyectoXamarin.Data.Repository
{
	public class ModelRepository : IRepositoryModel
	{
		public ModelRepository()
		{
		}

		//public async Task InitAsync()
		//{
		//    await App.DataBase.db.CreateTableAsync<Model>();
		//}

		public async Task<Model> GetByIdAsync(int id)
		{
			var Model = await App.DataBase.db.Table<Model>().Where(b => b.Id == id).FirstOrDefaultAsync();
			await App.DataBase.db.CloseAsync();
			return Model;
		}

		public async Task<List<Model>> GetAllAsync(bool forceRefresh = false)
		{
			var models = await App.DataBase.db.Table<Model>().ToListAsync();
			await App.DataBase.db.CloseAsync();
			return models;
		}

		public async Task<List<Model>> GetByBrandIdAsync(int brandId)
		{
			var Models = await App.DataBase.db.Table<Model>().Where(b => b.IdBrand == brandId).OrderBy(b => b.Name).ToListAsync();
			await App.DataBase.db.CloseAsync();
			return Models;
		}
	}
}