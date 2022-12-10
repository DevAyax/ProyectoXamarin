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
			var brand = await App.DataBase.connect.Table<Brand>().Where(b => b.Id == id).FirstOrDefaultAsync();
			await App.DataBase.connect.CloseAsync();
			return brand;
		}

		public async Task<List<Brand>> GetAllAsync(bool forceRefresh = false)
		{
			var brands = await App.DataBase.connect.Table<Brand>().OrderBy(b => b.Name).ToListAsync();
			return brands;
		}

		public async Task SetBrandsIntoDataBase()
		{
			string query = "INSERT INTO Brand (Name) VALUES('Abarth'),\r\n('Alfa Romeo'),\r\n('Aro'),\r\n('Asia'),\r\n('Asia Motors'),\r\n('Aston Martin'),\r\n('Audi'),\r\n('Austin'),\r\n('Auverland'),\r\n('Bentley'),\r\n('Bertone'),\r\n('Bmw'),\r\n('Cadillac'),\r\n('Chevrolet'),\r\n('Chrysler'),\r\n('Citroen'),\r\n('Corvette'),\r\n('Dacia'),\r\n('Daewoo'),\r\n('Daf'),\r\n('Daihatsu'),\r\n('Daimler'),\r\n('Dodge'),\r\n('Ferrari'),\r\n('Fiat'),\r\n('Ford'),\r\n('Galloper'),\r\n('Gmc'),\r\n('Honda'),\r\n('Hummer'),\r\n('Hyundai'),\r\n('Infiniti'),\r\n('Innocenti'),\r\n('Isuzu'),\r\n('Iveco'),\r\n('Iveco-pegaso'),\r\n('Jaguar'),\r\n('Jeep'),\r\n('Kia'),\r\n('Lada'),\r\n('Lamborghini'),\r\n('Lancia'),\r\n('Land-rover'),\r\n('Ldv'),\r\n('Lexus'),\r\n('Lotus'),\r\n('Mahindra'),\r\n('Maserati'),\r\n('Maybach'),\r\n('Mazda'),\r\n('Mercedes-benz'),\r\n('Mg'),\r\n('Mini'),\r\n('Mitsubishi'),\r\n('Morgan'),\r\n('Nissan'),\r\n('Opel'),\r\n('Peugeot'),\r\n('Pontiac'),\r\n('Porsche'),\r\n('Renault'),\r\n('Rolls-royce'),\r\n('Rover'),\r\n('Saab'),\r\n('Santana'),\r\n('Seat'),\r\n('Skoda'),\r\n('Smart'),\r\n('Ssangyong'),\r\n('Subaru'),\r\n('Suzuki'),\r\n('Talbot'),\r\n('Tata'),\r\n('Toyota'),\r\n('Umm'),\r\n('Vaz'),\r\n('Volkswagen'),\r\n('Volvo'),\r\n('Wartburg');\r\n\r\n";
			await App.DataBase.connect.QueryAsync<Brand>(query);
		}
	}
}