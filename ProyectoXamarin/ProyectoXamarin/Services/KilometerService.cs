using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ProyectoXamarin.Interfaces;
using ProyectoXamarin.Models.Kilometers;
using ProyectoXamarin.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(KilometerService))]

namespace ProyectoXamarin.Services
{
	public class KilometerService : IKilometerService
	{
		private readonly IKilometerRepository kilometerRepository;

		public KilometerService()
		{
			this.kilometerRepository = DependencyService.Get<IKilometerRepository>();
		}

		public async Task<ObservableCollection<Kilometer>> GetAllKilometersAsync(ObservableCollection<Kilometer> kilometers)
		{
			var kms = await kilometerRepository.GetAllAsync();

			if (kilometers.Count() > 0)
				kilometers.Clear();

			foreach (var km in kms)
			{
				kilometers.Add(km);
			}

			return kilometers;
		}

		public async Task SaveAsync(Kilometer entity)
		{
			var kilometer = await kilometerRepository.GetAsync(entity);
			if (kilometer == null)
			{
				var status = await kilometerRepository.SaveAsync(entity);

				if (status > 0)
				{
					await kilometerRepository.UpdateSesionDataKilometers(status, entity);
				}
			}
		}

		public async Task<Kilometer> GetKilometerAsync(int carId)
		{
			var kms = await kilometerRepository.GetByCarId(carId);
			var km = kms.Where(k => k.CarId == carId).OrderByDescending(c => c.Id).FirstOrDefault();

			return km;
		}

		public Task UpdateAsync(Kilometer entity)
		{
			throw new System.NotImplementedException();
		}
	}
}