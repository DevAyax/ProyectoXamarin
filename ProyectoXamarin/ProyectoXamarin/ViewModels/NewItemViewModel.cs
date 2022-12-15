// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 11-23-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="NewItemViewModel.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using ProyectoXamarin.Models;
using Xamarin.Forms;

namespace ProyectoXamarin.ViewModels
{
	/// <summary>
	/// Class NewItemViewModel.
	/// Implements the <see cref="ProyectoXamarin.ViewModels.BaseViewModel" />
	/// </summary>
	/// <seealso cref="ProyectoXamarin.ViewModels.BaseViewModel" />
	public class NewItemViewModel : BaseViewModel
	{
		/// <summary>
		/// The text
		/// </summary>
		private string text;
		/// <summary>
		/// The description
		/// </summary>
		private string description;

		/// <summary>
		/// Initializes a new instance of the <see cref="NewItemViewModel"/> class.
		/// </summary>
		public NewItemViewModel()
		{
			SaveCommand = new Command(OnSave, ValidateSave);
			CancelCommand = new Command(OnCancel);
			this.PropertyChanged +=
				(_, __) => SaveCommand.ChangeCanExecute();
		}

		/// <summary>
		/// Validates the save.
		/// </summary>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		private bool ValidateSave()
		{
			return !String.IsNullOrWhiteSpace(text)
				&& !String.IsNullOrWhiteSpace(description);
		}

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text
		{
			get => text;
			set => SetProperty(ref text, value);
		}

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description
		{
			get => description;
			set => SetProperty(ref description, value);
		}

		/// <summary>
		/// Gets the save command.
		/// </summary>
		/// <value>The save command.</value>
		public Command SaveCommand { get; }
		/// <summary>
		/// Gets the cancel command.
		/// </summary>
		/// <value>The cancel command.</value>
		public Command CancelCommand { get; }

		/// <summary>
		/// Called when [cancel].
		/// </summary>
		private async void OnCancel()
		{
			// This will pop the current page off the navigation stack
			await Shell.Current.GoToAsync("..");
		}

		/// <summary>
		/// Called when [save].
		/// </summary>
		private async void OnSave()
		{
			Item newItem = new Item()
			{
				Id = Guid.NewGuid().ToString(),
				Text = Text,
				Description = Description
			};

			await DataStore.AddItemAsync(newItem);

			// This will pop the current page off the navigation stack
			await Shell.Current.GoToAsync("..");
		}
	}
}