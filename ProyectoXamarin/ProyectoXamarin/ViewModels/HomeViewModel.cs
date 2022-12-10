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
		private string infoDate;

		private string kilometers;

		private string infoCar;

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

		public async Task OnAppearingAsync()
		{
			await GetAllInfo();
			await GetPromptAsync();
		}

		public async Task GetAllInfo()
		{
			var user = await userService.GetUserAsync(SesionData.UserId);

			if (user.CarId != null)
			{
				var _Car = await carService.GetCarByUserAsync(user.Id);
				var brand = await carService.GetBrandAsync((int) _Car.BrandId);
				var model = await carService.GetModelAsync((int) _Car.ModelId);
				var _Kilometer = await kilometerService.GetKilometerAsync(_Car.Id);

				SesionData.CarId = _Car != null ? _Car.Id : 0;
				SesionData.kilometers = _Kilometer != null ? (int) _Kilometer.Km : 0;

				Kilometers = $"{SesionData.kilometers} Km";
				InfoCar = $"({brand.Name}) ({model.Name})";
				InfoDate = $"Útima actualización: {_Kilometer.DateCreation}";
			}
		}

		public async Task GetPromptAsync()
		{
			var car = await carService.GetCarByUserAsync(SesionData.UserId);
			if (car != null)
			{
				var promptConfig = new PromptConfig();
				promptConfig.InputType = InputType.Number;
				promptConfig.IsCancellable = true;
				promptConfig.Message = "Actualiza kilometros";
				promptConfig.Placeholder = $"{SesionData.kilometers}";
				var result = await UserDialogs.Instance.PromptAsync(promptConfig);

				if (result.Ok)
				{
					SesionData.kilometers = int.Parse(result.Text);

					await kilometerService.SaveAsync(new Kilometer
					{
						Km = SesionData.kilometers,
						CarId = car.Id,
						DateCreation = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")
					});

					Kilometers = $"{SesionData.kilometers} Km";

					car.Km = SesionData.kilometers;

					await carService.UpdateAsync(car);
				}
			}
		}

		public Command AddKilometersCommand { get; set; }

		public string Kilometers
		{
			get => kilometers;
			set
			{
				kilometers = value;
				OnPropertyChanged();
			}
		}

		public string InfoCar
		{
			get => infoCar;
			set
			{
				infoCar = value;
				OnPropertyChanged();
			}
		}

		public string InfoDate
		{
			get => infoDate;
			set
			{
				infoDate = value;
				OnPropertyChanged();
			}
		}
	}
}