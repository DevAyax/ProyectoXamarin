﻿using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Acr.UserDialogs;
using ProyectoXamarin.Interfaces;
using ProyectoXamarin.Models.Brands;
using ProyectoXamarin.Models.Cars;
using ProyectoXamarin.Models.Kilometers;
using ProyectoXamarin.Models.ModelCar;
using ProyectoXamarin.Views;
using Xamarin.Forms;

namespace ProyectoXamarin.ViewModels
{
	public class CreateCarViewModel : BaseViewModel
	{
		private ObservableCollection<Model> models;

		private ObservableCollection<Brand> brands;

		private int kilometers;

		private string combustibleSelected;

		private string doorsSelected;

		private Car newCar;

		private Brand brandSelected;

		private Model modelSelected;

		private readonly ICarService carService;

		private readonly IKilometerService kilometerService;

		public CreateCarViewModel()
		{
			this.carService = DependencyService.Get<ICarService>();
			this.kilometerService = DependencyService.Get<IKilometerService>();
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

		protected override async void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName == nameof(brandSelected))
			{
				await GetModels();
			}
		}

		public async Task OnAppearingAsync()
		{
			var brands = await carService.GetAllBrandsAsync(new ObservableCollection<Brand>());

			if (brands.Count == 0)
			{
				await carService.SetDataIntoDataBase();
			}
			
			await GetBrands(brands);
			await FillFields();
		}

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
				DoorsSelected = car.Doors != null ? car.Doors : "";
				CombustibleSelected = car.Combustible != null ? car.Combustible : "";
			}
		}

		public async Task GetBrands(ObservableCollection<Brand> brands)
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

		public async Task GetModels()
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

		public async Task UpdateCar()
		{
			if (IsBusy)
				return;
			try
			{
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

		public async Task SaveCar()
		{
			if (IsBusy)
				return;
			var cars = await carService.GetAllCarsAsync(new ObservableCollection<Car>());

			try
			{
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

		public Command CarRegisterCommand { get; set; }
		public Command AddKilometersCommand { get; set; }

		public ObservableCollection<Brand> Brands
		{
			get => brands;
			set
			{
				brands = value;
				OnPropertyChanged();
			}
		}

		public ObservableCollection<Model> Models
		{
			get => models;
			set
			{
				models = value;
				OnPropertyChanged();
			}
		}

		public int Kilometers
		{
			get => kilometers;
			set
			{
				kilometers = value;
				OnPropertyChanged();
			}
		}

		public string DoorsSelected
		{
			get => doorsSelected;
			set
			{
				doorsSelected = value;
				OnPropertyChanged();
			}
		}

		public string CombustibleSelected
		{
			get => combustibleSelected;
			set
			{
				combustibleSelected = value;
				OnPropertyChanged();
			}
		}

		public Car NewCar
		{
			get => newCar;
			set
			{
				newCar = value;
				OnPropertyChanged();
			}
		}

		public Brand BrandSelected
		{
			get => brandSelected;
			set
			{
				brandSelected = value;
				OnPropertyChanged(nameof(brandSelected));
			}
		}

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