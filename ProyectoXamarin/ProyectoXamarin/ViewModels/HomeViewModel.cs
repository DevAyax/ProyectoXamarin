// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-05-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-14-2022
// ***********************************************************************
// <copyright file="HomeViewModel.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using ProyectoXamarin.Interfaces;
using ProyectoXamarin.Models.Kilometers;
using ProyectoXamarin.Services;
using Xamarin.Forms;

namespace ProyectoXamarin.ViewModels
{
	/// <summary>
	/// Class HomeViewModel.
	/// Implements the <see cref="ProyectoXamarin.ViewModels.BaseViewModel" />
	/// </summary>
	/// <seealso cref="ProyectoXamarin.ViewModels.BaseViewModel" />
	public class HomeViewModel : BaseViewModel
	{
		/// <summary>
		/// The information date
		/// </summary>
		private string infoDate;

		/// <summary>
		/// The kilometers
		/// </summary>
		private string kilometers;

		/// <summary>
		/// The information car
		/// </summary>
		private string infoCar;

		/// <summary>
		/// The selected kilometer
		/// </summary>
		private string selectedKilometer;

		/// <summary>
		/// The image
		/// </summary>
		private ImageSource image;

		/// <summary>
		/// The kilometers list
		/// </summary>
		private ObservableCollection<Kilometer> kilometersList;

		/// <summary>
		/// The car service
		/// </summary>
		private readonly ICarService carService;

		/// <summary>
		/// The kilometer service
		/// </summary>
		private readonly IKilometerService kilometerService;

		/// <summary>
		/// The user service
		/// </summary>
		private readonly IUserService userService;

		/// <summary>
		/// Initializes a new instance of the <see cref="HomeViewModel"/> class.
		/// </summary>
		public HomeViewModel()
		{
			carService = DependencyService.Get<ICarService>();
			kilometerService = DependencyService.Get<IKilometerService>();
			userService = DependencyService.Get<IUserService>();

			KilometersList = new ObservableCollection<Kilometer>();
			kilometers = string.Empty;
			InfoCar = string.Empty;
			image = "";
			
			AddKilometersCommand = new Command(async () =>
			{
				await GetPromptAsync();
			});

			RefreshCommand = new Command(async () =>
			{
				IsBusy = true;
				KilometersList = await kilometerService.GetAllKilometersAsync(KilometersList);
				IsBusy = false;
			});
		}

		/// <summary>
		/// On appearing as an asynchronous operation.
		/// </summary>
		/// <returns>A Task representing the asynchronous operation.</returns>
		public async Task OnAppearingAsync()
		{
			UserDialogs.Instance.ShowLoading();
			
			if (SesionData.userId == 0)
				await userService.AutoLoginAsync();

			await GetAllInfo();

			KilometersList = await kilometerService.GetAllKilometersAsync(KilometersList);

			UserDialogs.Instance.HideLoading();
		}

		/// <summary>
		/// Gets all information.
		/// </summary>
		public async Task GetAllInfo()
		{
			if (IsBusy)
				return;
			try
			{
				UserDialogs.Instance.HideLoading();
				IsBusy = true;
				var user = await userService.GetUserAsync(SesionData.userId);

				if (user.CarId != null)
				{
					var car = await carService.GetCarByUserAsync(user.Id);
					var brand = await carService.GetBrandAsync((int) car.BrandId);
					var model = await carService.GetModelAsync((int) car.ModelId);
					var _Kilometer = await kilometerService.GetKilometerAsync(car.Id);

					SesionData.carId = car != null ? car.Id : 0;
					SesionData.kilometers = _Kilometer != null ? (int) _Kilometer.Km : 0;
					Uri url = new Uri($"https://cdn.imagin.studio/getImage?&customer=esbernabeu&make={brand.Name}&modelFamily={model.Name}&modelRange={model.Name}&modelVariant=ca&modelYear=2021&powerTrain=fossil&transmission=0&bodySize=2&trim=0&paintId=imagin-yellow&angle=22");

					Image = url;
					Kilometers = $"{SesionData.kilometers} Km";
					InfoCar = $"({brand.Name}) ({model.Name})";
					InfoDate = $"Útima actualización: {_Kilometer.DateCreation}";

					if (car.Km <= 0)
						await GetPromptAsync();
				}

				IsBusy = false;
				UserDialogs.Instance.HideLoading();
			}
			catch (Exception ex)
			{
				await UserDialogs.Instance.AlertAsync(ex.StackTrace, "ERROR", "OK");
				IsBusy = false;
				throw;
			}
			finally { IsBusy = false; }
		}

		/// <summary>
		/// Get prompt as an asynchronous operation.
		/// </summary>
		/// <returns>A Task representing the asynchronous operation.</returns>
		public async Task GetPromptAsync()
		{
			if (IsBusy)
				return;

			try
			{
				IsBusy = true;

				var car = await carService.GetCarByUserAsync(SesionData.userId);

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
				
				IsBusy = false;
			}
			catch (Exception ex)
			{
				await UserDialogs.Instance.AlertAsync(ex.StackTrace, "ERROR", "OK");
				IsBusy = false;
				throw;
			}
			finally { IsBusy = false; }
		}

		/// <summary>
		/// Gets or sets the add kilometers command.
		/// </summary>
		/// <value>The add kilometers command.</value>
		public Command AddKilometersCommand { get; set; }

		/// <summary>
		/// Gets or sets the refresh command.
		/// </summary>
		/// <value>The refresh command.</value>
		public Command RefreshCommand { get; set; }

		/// <summary>
		/// Gets or sets the kilometers list.
		/// </summary>
		/// <value>The kilometers list.</value>
		public ObservableCollection<Kilometer> KilometersList
		{
			get => kilometersList;
			set
			{
				kilometersList = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the selected kilometer.
		/// </summary>
		/// <value>The selected kilometer.</value>
		public string SelectedKilometer
		{
			get => selectedKilometer;
			set
			{
				selectedKilometer = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the image.
		/// </summary>
		/// <value>The image.</value>
		public ImageSource Image
		{
			get => image;
			set
			{
				image = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the kilometers.
		/// </summary>
		/// <value>The kilometers.</value>
		public string Kilometers
		{
			get => kilometers;
			set
			{
				kilometers = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the information car.
		/// </summary>
		/// <value>The information car.</value>
		public string InfoCar
		{
			get => infoCar;
			set
			{
				infoCar = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the information date.
		/// </summary>
		/// <value>The information date.</value>
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