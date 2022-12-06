using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProyectoXamarin.Models.Kilometers;

namespace ProyectoXamarin.Interfaces
{
	public interface IKilometerService :IService<Kilometer>
	{
		Task<ObservableCollection<Kilometer>> GetAllKilometersAsync(ObservableCollection<Kilometer> kilometers);
		Task<Kilometer> GetKilometerAsync(int carId);
	}
}
