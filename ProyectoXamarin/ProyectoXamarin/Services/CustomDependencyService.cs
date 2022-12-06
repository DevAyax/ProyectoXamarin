using ProyectoXamarin.Intetrfaces;
using Xamarin.Forms;

namespace ProyectoXamarin.Services
{
	public class CustomDependencyService : ICustomDependencyService
	{
		public T Get<T>() where T : class
		{
			return DependencyService.Get<T>();
		}
	}
}
