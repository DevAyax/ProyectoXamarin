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
        //SQLiteAsyncConnection db;

        public BrandRepository()
        {
        }

        //public async Task InitAsync()
        //{
        //    var dataBase = new DataBase();
        //    db = dataBase.DbConnection();
        //    await App.DataBase.db.CreateTableAsync<Brand>();
        //}

        public async Task<Brand> GetByIdAsync(int id)
        {
            var brand = await App.DataBase.db.Table<Brand>().Where(b => b.Id == id).FirstOrDefaultAsync();
            await App.DataBase.db.CloseAsync();
            return brand;
        }

        public async Task<List<Brand>> GetAllAsync(bool forceRefresh = false)
        {
			App.DataBase.db.CreateTableAsync<Brand>().Wait();
			var brands = await App.DataBase.db.Table<Brand>().OrderBy(b => b.Name).ToListAsync();
            return brands;
        }
    }
}
