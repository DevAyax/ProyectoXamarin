// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 11-26-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-13-2022
// ***********************************************************************
// <copyright file="CreateCarViewModel.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Acr.UserDialogs;
using ProyectoXamarin.Interfaces;
using ProyectoXamarin.Models.Brands;
using ProyectoXamarin.Models.Cars;
using ProyectoXamarin.Models.Kilometers;
using ProyectoXamarin.Models.ModelCar;
using ProyectoXamarin.Services;
using ProyectoXamarin.Views;
using Xamarin.Forms;

namespace ProyectoXamarin.ViewModels
{
	/// <summary>
	/// Class CreateCarViewModel.
	/// Implements the <see cref="ProyectoXamarin.ViewModels.BaseViewModel" />
	/// </summary>
	/// <seealso cref="ProyectoXamarin.ViewModels.BaseViewModel" />
	public class CreateCarViewModel : BaseViewModel
	{
		/// <summary>
		/// The models
		/// </summary>
		private ObservableCollection<Model> models;

		/// <summary>
		/// The brands
		/// </summary>
		private ObservableCollection<Brand> brands;

		/// <summary>
		/// The kilometers
		/// </summary>
		private int kilometers;

		/// <summary>
		/// The combustible selected
		/// </summary>
		private string combustibleSelected;

		/// <summary>
		/// The doors selected
		/// </summary>
		private string doorsSelected;

		/// <summary>
		/// The new car
		/// </summary>
		private Car newCar;

		/// <summary>
		/// The brand selected
		/// </summary>
		private Brand brandSelected;

		/// <summary>
		/// The model selected
		/// </summary>
		private Model modelSelected;

		/// <summary>
		/// The car service
		/// </summary>
		private readonly ICarService carService;

		/// <summary>
		/// The kilometer service
		/// </summary>
		private readonly IKilometerService kilometerService;

		private readonly IUserService userService;

		/// <summary>
		/// Initializes a new instance of the <see cref="CreateCarViewModel"/> class.
		/// </summary>
		public CreateCarViewModel()
		{
			this.carService = DependencyService.Get<ICarService>();
			this.kilometerService = DependencyService.Get<IKilometerService>();
			this.userService = DependencyService.Get<IUserService>();

			Brands = new ObservableCollection<Brand>();
			
			Models = new ObservableCollection<Model>();
			
			NewCar = new Car();
			
			Kilometers = 0;
			
			CarRegisterCommand = new Command(async () =>
			{
				if (NewCar.Id == 0)
					await SaveCar();
				else
					await UpdateCar();
			});
			
			AddKilometersCommand = new Command(async () =>
			{
				if (NewCar.Id == 0)
					await SaveCar();
				else
					await UpdateCar();
			});
		}

		/// <summary>
		/// Called when [property changed].
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		protected override async void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName == nameof(brandSelected))
			{
				await GetModelsByBrandAsync();
			}
		}

		/// <summary>
		/// On appearing as an asynchronous operation.
		/// </summary>
		/// <returns>A Task representing the asynchronous operation.</returns>
		public async Task OnAppearingAsync()
		{
			UserDialogs.Instance.ShowLoading();
			if (SesionData.UserId == 0)
				await userService.AutoLoginAsync();

			var brands = await carService.GetAllBrandsAsync(new ObservableCollection<Brand>());

			if (brands.Count == 0)
			{
				await carService.SetDataIntoDataBase();
				brands = await carService.GetAllBrandsAsync(new ObservableCollection<Brand>());
			}
			
			await SetBrands(brands);
			await FillFields();
			UserDialogs.Instance.HideLoading();
		}

		/// <summary>
		/// Fills the fields.
		/// </summary>
		public async Task FillFields()
		{
			var car = await carService.GetCarByUserAsync(SesionData.UserId);
			if (car != null)
			{
				var brand = await carService.GetBrandAsync((int) car.BrandId);
				var model = await carService.GetModelAsync((int) car.ModelId);

				// Rellenar datos
				BrandSelected = brand;
				ModelSelected = model;
				Kilometers = (int) car.Km;
				DoorsSelected = car.Doors ?? "";
				CombustibleSelected = car.Combustible != null ? car.Combustible : "";
			}
		}

		/// <summary>
		/// Gets the brands.
		/// </summary>
		/// <param name="brands">The brands.</param>
		public async Task SetBrands(ObservableCollection<Brand> brands)
		{
			if (IsBusy)
				Brands.Clear();

			try
			{
				IsBusy = true;
				Brands = brands;
				IsBusy = false;
			}
			catch (Exception ex)
			{
				Brands.Clear();
				IsBusy = false;
				await UserDialogs.Instance.AlertAsync($"{ex.Message}", "ERROR", "OK");
			}
			finally
			{
				IsBusy = false;
			}
		}

		/// <summary>
		/// Gets the models.
		/// </summary>
		public async Task GetModelsByBrandAsync()
		{
			if (IsBusy)
				Models.Clear();

			try
			{
				IsBusy = true;
				Models = await carService.GetAllModelsAsync(Models, BrandSelected.Id);
				IsBusy = false;
			}
			catch (Exception ex)
			{
				Models.Clear();
				IsBusy = false;
				await UserDialogs.Instance.AlertAsync($"{ex.Message}", "ERROR", "OK");
			}
			finally
			{
				IsBusy = false;
			}
		}

		/// <summary>
		/// Updates the car.
		/// </summary>
		public async Task UpdateCar()
		{
			if (IsBusy)
				return;
			try
			{
				UserDialogs.Instance.ShowLoading();
				IsBusy = true;

				if (brandSelected != null & modelSelected != null)
				{
					await carService.UpdateAsync(NewCar);
					await kilometerService.SaveAsync(new Kilometer { CarId = NewCar.Id, DateCreation = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), Km = NewCar.Km });
				}
				else
				{
					await UserDialogs.Instance.AlertAsync("Compruebe que ha seleccionado marca y modelo", "INFO", "OK");
				}
				UserDialogs.Instance.HideLoading();
			}
			catch (Exception ex)
			{
				IsBusy = false;
				await UserDialogs.Instance.AlertAsync($"{ex.Message}", "ERROR", "OK");
			}
			finally
			{
				IsBusy = false;
			}
		}

		/// <summary>
		/// Saves the car.
		/// </summary>
		public async Task SaveCar()
		{
			if (IsBusy)
				return;

			try
			{
				UserDialogs.Instance.ShowLoading();
				IsBusy = true;

				if (brandSelected != null & modelSelected != null)
				{
					NewCar.BrandId = brandSelected.Id;
					NewCar.UserId = SesionData.UserId;
					NewCar.ModelId = modelSelected.Id;
					NewCar.Km = Kilometers;
					NewCar.Combustible = CombustibleSelected;
					NewCar.Doors = DoorsSelected;

					await carService.SaveAsync(NewCar);
					await kilometerService.SaveAsync(new Kilometer { CarId = NewCar.Id, DateCreation = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), Km = NewCar.Km });
				}
				else
				{
					await UserDialogs.Instance.AlertAsync("Compruebe que ha seleccionado marca y modelo", "INFO", "OK");
				}

				IsBusy = false;
				
				UserDialogs.Instance.HideLoading();

				await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
			}
			catch (Exception ex)
			{
				IsBusy = false;
				await UserDialogs.Instance.AlertAsync($"{ex.Message}", "ERROR", "OK");
			}
			finally
			{
				IsBusy = false;
			}
		}

		/// <summary>
		/// Gets or sets the car register command.
		/// </summary>
		/// <value>The car register command.</value>
		public Command CarRegisterCommand { get; set; }

		/// <summary>
		/// Gets or sets the add kilometers command.
		/// </summary>
		/// <value>The add kilometers command.</value>
		public Command AddKilometersCommand { get; set; }

		/// <summary>
		/// Gets or sets the brands.
		/// </summary>
		/// <value>The brands.</value>
		public ObservableCollection<Brand> Brands
		{
			get => brands;
			set
			{
				brands = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the models.
		/// </summary>
		/// <value>The models.</value>
		public ObservableCollection<Model> Models
		{
			get => models;
			set
			{
				models = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the kilometers.
		/// </summary>
		/// <value>The kilometers.</value>
		public int Kilometers
		{
			get => kilometers;
			set
			{
				kilometers = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the doors selected.
		/// </summary>
		/// <value>The doors selected.</value>
		public string DoorsSelected
		{
			get => doorsSelected;
			set
			{
				doorsSelected = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the combustible selected.
		/// </summary>
		/// <value>The combustible selected.</value>
		public string CombustibleSelected
		{
			get => combustibleSelected;
			set
			{
				combustibleSelected = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Creates new car.
		/// </summary>
		/// <value>The new car.</value>
		public Car NewCar
		{
			get => newCar;
			set
			{
				newCar = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the brand selected.
		/// </summary>
		/// <value>The brand selected.</value>
		public Brand BrandSelected
		{
			get => brandSelected;
			set
			{
				brandSelected = value;
				OnPropertyChanged(nameof(brandSelected));
			}
		}

		/// <summary>
		/// Gets or sets the model selected.
		/// </summary>
		/// <value>The model selected.</value>
		public Model ModelSelected
		{
			get => modelSelected;
			set
			{
				modelSelected = value;
				OnPropertyChanged();
			}
		}
	}
}