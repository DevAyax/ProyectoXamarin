namespace ProyectoXamarin.Intetrfaces
{
	public interface ICustomDependencyService
	{
		T Get<T>() where T : class;
	}
}