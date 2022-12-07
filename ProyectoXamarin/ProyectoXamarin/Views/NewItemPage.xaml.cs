using ProyectoXamarin.Models;
using ProyectoXamarin.ViewModels;
using Xamarin.Forms;

namespace ProyectoXamarin.Views
{
	public partial class NewItemPage : ContentPage
	{
		public Item Item { get; set; }

		public NewItemPage()
		{
			InitializeComponent();
			BindingContext = new NewItemViewModel();
		}
	}
}