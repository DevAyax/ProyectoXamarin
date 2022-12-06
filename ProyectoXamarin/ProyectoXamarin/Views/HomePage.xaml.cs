﻿using ProyectoXamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoXamarin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		public HomePage()
		{
			InitializeComponent();
		}

		protected override async void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			if (BindingContext is HomeViewModel homeViewModel)
			{
				await homeViewModel.OnActivatedAsync();
			}
		}
	}
}