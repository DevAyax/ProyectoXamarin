// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-03-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-14-2022
// ***********************************************************************
// <copyright file="BrandRepository.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoXamarin.Data.Repository;
using ProyectoXamarin.Models.Brands;
using Xamarin.Forms;

[assembly: Dependency(typeof(BrandRepository))]

namespace ProyectoXamarin.Data.Repository
{

	/// <summary>
	/// Class BrandRepository.
	/// Implements the <see cref="IBrandRepository" />
	/// </summary>
	/// <seealso cref="IBrandRepository" />
	public class BrandRepository : IBrandRepository
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BrandRepository"/> class.
		/// </summary>
		public BrandRepository()
		{
		}

		/// <summary>
		/// Get by identifier as an asynchronous operation.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>A Task<see cref="Task{ProyectoXamarin.Models.Brands.Brand}"/> representing the asynchronous operation.</returns>
		public async Task<Brand> GetByIdAsync(int id)
		{
			var brand = await App.DataBase.connect.Table<Brand>().Where(b => b.Id == id).FirstOrDefaultAsync();
			await App.DataBase.connect.CloseAsync();
			return brand;
		}

		/// <summary>
		/// Get all as an asynchronous operation.
		/// </summary>
		/// <param name="forceRefresh">if set to <c>true</c> [force refresh].</param>
		/// <returns>A Task<see cref="Task{List{ProyectoXamarin.Models.Brands.Brand}}"/>; representing the asynchronous operation.</returns>
		public async Task<List<Brand>> GetAllAsync(bool forceRefresh = false)
		{
			var brands = await App.DataBase.connect.Table<Brand>().OrderBy(b => b.Name).ToListAsync();
			return brands;
		}

		/// <summary>
		/// Defines the <see cref="SetBrandsIntoDataBase" />.
		/// </summary>
		/// <returns>Task<see cref="Task"/>.</returns>
		public async Task SetBrandsIntoDataBase()
		{
			string query = "INSERT INTO Brand (Name) VALUES('Abarth'),\r\n('Alfa Romeo'),\r\n('Aro'),\r\n('Asia'),\r\n('Asia Motors'),\r\n('Aston Martin'),\r\n('Audi'),\r\n('Austin'),\r\n('Auverland'),\r\n('Bentley'),\r\n('Bertone'),\r\n('Bmw'),\r\n('Cadillac'),\r\n('Chevrolet'),\r\n('Chrysler'),\r\n('Citroen'),\r\n('Corvette'),\r\n('Dacia'),\r\n('Daewoo'),\r\n('Daf'),\r\n('Daihatsu'),\r\n('Daimler'),\r\n('Dodge'),\r\n('Ferrari'),\r\n('Fiat'),\r\n('Ford'),\r\n('Galloper'),\r\n('Gmc'),\r\n('Honda'),\r\n('Hummer'),\r\n('Hyundai'),\r\n('Infiniti'),\r\n('Innocenti'),\r\n('Isuzu'),\r\n('Iveco'),\r\n('Iveco-pegaso'),\r\n('Jaguar'),\r\n('Jeep'),\r\n('Kia'),\r\n('Lada'),\r\n('Lamborghini'),\r\n('Lancia'),\r\n('Land-rover'),\r\n('Ldv'),\r\n('Lexus'),\r\n('Lotus'),\r\n('Mahindra'),\r\n('Maserati'),\r\n('Maybach'),\r\n('Mazda'),\r\n('Mercedes-benz'),\r\n('Mg'),\r\n('Mini'),\r\n('Mitsubishi'),\r\n('Morgan'),\r\n('Nissan'),\r\n('Opel'),\r\n('Peugeot'),\r\n('Pontiac'),\r\n('Porsche'),\r\n('Renault'),\r\n('Rolls-royce'),\r\n('Rover'),\r\n('Saab'),\r\n('Santana'),\r\n('Seat'),\r\n('Skoda'),\r\n('Smart'),\r\n('Ssangyong'),\r\n('Subaru'),\r\n('Suzuki'),\r\n('Talbot'),\r\n('Tata'),\r\n('Toyota'),\r\n('Umm'),\r\n('Vaz'),\r\n('Volkswagen'),\r\n('Volvo'),\r\n('Wartburg');\r\n\r\n";
			await App.DataBase.connect.QueryAsync<Brand>(query);
		}
	}
}