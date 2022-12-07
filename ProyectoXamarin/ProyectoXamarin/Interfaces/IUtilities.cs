using System.Threading.Tasks;

namespace ProyectoXamarin.Interfaces
{
	public interface IUtilities
	{
		Task GetSatatus(int state, string toast);
	}
}