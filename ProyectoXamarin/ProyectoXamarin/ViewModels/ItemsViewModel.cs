// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 11-23-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="ItemsViewModel.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using ProyectoXamarin.Models;
using ProyectoXamarin.Views;
using Xamarin.Forms;

namespace ProyectoXamarin.ViewModels
{
	/// <summary>
	/// Class ItemsViewModel.
	/// Implements the <see cref="ProyectoXamarin.ViewModels.BaseViewModel" />
	/// </summary>
	/// <seealso cref="ProyectoXamarin.ViewModels.BaseViewModel" />
	public class ItemsViewModel : BaseViewModel
	{
		/// <summary>
		/// The selected item
		/// </summary>
		private Item _selectedItem;

		/// <summary>
		/// Gets the items.
		/// </summary>
		/// <value>The items.</value>
		public ObservableCollection<Item> Items { get; }

		/// <summary>
		/// Gets the load items command.
		/// </summary>
		/// <value>The load items command.</value>
		public Command LoadItemsCommand { get; }

		/// <summary>
		/// Gets the add item command.
		/// </summary>
		/// <value>The add item command.</value>
		public Command AddItemCommand { get; }

		/// <summary>
		/// Gets the item tapped.
		/// </summary>
		/// <value>The item tapped.</value>
		public Command<Item> ItemTapped { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ItemsViewModel"/> class.
		/// </summary>
		public ItemsViewModel()
		{
			Title = "Browse";
			Items = new ObservableCollection<Item>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

			ItemTapped = new Command<Item>(OnItemSelected);

			AddItemCommand = new Command(OnAddItem);
		}

		/// <summary>
		/// Executes the load items command.
		/// </summary>
		private async Task ExecuteLoadItemsCommand()
		{
			IsBusy = true;

			try
			{
				Items.Clear();
				var items = await DataStore.GetItemsAsync(true);
				foreach (var item in items)
				{
					Items.Add(item);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

		/// <summary>
		/// Called when [appearing].
		/// </summary>
		public void OnAppearing()
		{
			IsBusy = true;
			SelectedItem = null;
		}

		/// <summary>
		/// Gets or sets the selected item.
		/// </summary>
		/// <value>The selected item.</value>
		public Item SelectedItem
		{
			get => _selectedItem;
			set
			{
				SetProperty(ref _selectedItem, value);
				OnItemSelected(value);
			}
		}

		/// <summary>
		/// Called when [add item].
		/// </summary>
		/// <param name="obj">The object.</param>
		private async void OnAddItem(object obj)
		{
			await Shell.Current.GoToAsync(nameof(NewItemPage));
		}

		/// <summary>
		/// Called when [item selected].
		/// </summary>
		/// <param name="item">The item.</param>
		private async void OnItemSelected(Item item)
		{
			if (item == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
		}
	}
}