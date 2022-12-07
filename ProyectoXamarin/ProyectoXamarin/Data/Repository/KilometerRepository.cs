using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoXamarin.Data.Repository;
using ProyectoXamarin.Models.Kilometers;
using Xamarin.Forms;

[assembly: Dependency(typeof(KilometerRepository))]

namespace ProyectoXamarin.Data.Repository
{
	public class KilometerRepository : IKilometerRepository
	{
		//SQLiteAsyncConnection db;

		public KilometerRepository()
		{
		}

		//public async Task InitAsync()
		//{
		//	var dataBase = new DataBase();
		//	db = dataBase.DbConnection();
		//	await App.DataBase.db.CreateTableAsync<Kilometer>();
		//}

		public async Task<List<Kilometer>> GetAllAsync(bool forceRefresh = false)
		{
			return await App.DataBase.db.Table<Kilometer>().OrderBy(k => k.DateCreation).ToListAsync();
		}

		public async Task<Kilometer> GetAsync(Kilometer km)
		{
			return await App.DataBase.db.Table<Kilometer>().FirstOrDefaultAsync(k => k.DateCreation == km.DateCreation & k.Km == km.Km);
		}

		public async Task<Kilometer> GetByIdAsync(int id)
		{
			return await App.DataBase.db.Table<Kilometer>().FirstOrDefaultAsync(k => k.Id == id);
		}

		public async Task<List<Kilometer>> GetByCarId(int carId)
		{
			return await App.DataBase.db.Table<Kilometer>().Where(k => k.CarId == carId).ToListAsync();
		}

		public async Task<int> SaveAsync(Kilometer kilometer)
		{
			int status = 0;

			status = await App.DataBase.db.InsertAsync(kilometer);
			await UpdateSesionDataKilometers(status, kilometer);

			return status;
		}

		public async Task UpdateSesionDataKilometers(int status, Kilometer kilometer)
		{
			if (status == 1)
			{
				var _kilometer = await App.DataBase.db.Table<Kilometer>().Where(k => k.Km == kilometer.Km).FirstOrDefaultAsync();
				SesionData.kilometers = (int) _kilometer.Km;
			}
		}
	}
}