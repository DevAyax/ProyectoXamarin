using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using ProyectoXamarin.Interfaces;
using ProyectoXamarin.Models.Kilometers;
using Xamarin.Forms;

namespace ProyectoXamarin.ViewModels
{
	public class HomeViewModel : BaseViewModel
	{

		public Command AddKilometersCommand { get; set; }

		private string kilometers;
		public string Kilometers
		{
			get => kilometers;
			set
			{
				kilometers = value;
				OnPropertyChanged();
			}
		}

		private string infoCar;
		public string InfoCar
		{
			get => infoCar;
			set
			{
				infoCar = value;
				OnPropertyChanged();
			}
		}

		private string infoDate;
		public string InfoDate
		{
			get => infoDate;
			set
			{
				infoDate = value;
				OnPropertyChanged();
			}
		}

		private readonly ICarService carService;
		private readonly IKilometerService kilometerService;
		private readonly IUserService userService;

		public HomeViewModel()
		{
			carService = DependencyService.Get<ICarService>();
			kilometerService = DependencyService.Get<IKilometerService>();
			userService = DependencyService.Get<IUserService>();
			kilometers = string.Empty;
			InfoCar = string.Empty;

			AddKilometersCommand = new Command(async () =>
			{
				await GetPromptAsync();
			});
		}
		public async Task OnActivatedAsync()
		{
			//await carService.InitAsync();
			//await kilometerService.InitAsync();
			//await userService.InitAsync();
			await GetAllInfo();
		}

		public async Task GetAllInfo()
		{
			var user = await userService.GetUserAsync(Constants.UserId);
			if (user.CarId != null)
			{
				var _Car = await carService.GetCarByUserAsync(user.Id);
				var brand = await carService.GetBrandAsync((int) _Car.BrandId);
				var model = await carService.GetModelAsync((int) _Car.ModelId);
				var _Kilometer = await kilometerService.GetKilometerAsync(_Car.Id);

				Constants.CarId = _Car != null ? _Car.Id : 0;
				Constants.kilometers = _Kilometer != null ? (int) _Kilometer.Km : 0;
				Constants.kilometerEntity = _Kilometer != null ? _Kilometer : null;

				Kilometers = $"{Constants.kilometers} Km";
				InfoCar = $"({brand.Name}) ({model.Name})";
				InfoDate = $"Útima actualización: {_Kilometer.DateCreation}";
			}
		}

		public async Task GetPromptAsync()
		{
			var _Car = await carService.GetCarByUserAsync(Constants.UserId);

			var promptConfig = new PromptConfig();
			promptConfig.InputType = InputType.Number;
			promptConfig.IsCancellable = true;
			promptConfig.Message = "Actualiza kilometros";
			promptConfig.Placeholder = $"{Constants.kilometers}";
			var result = await UserDialogs.Instance.PromptAsync(promptConfig);

			if (result.Ok)
			{
				Constants.kilometers = int.Parse(result.Text);
				
				await kilometerService.SaveAsync(new Kilometer
				{
					Km = Constants.kilometers,
					CarId = _Car.Id,
					DateCreation = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")
				});
				
				Kilometers = $"{Constants.kilometers} Km";
				
				_Car.Km = Constants.kilometers;
				
				await carService.UpdateAsync(_Car);
			}
		}

	}
}
