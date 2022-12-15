// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 11-23-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="NewItemPage.xaml.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using ProyectoXamarin.Models;
using ProyectoXamarin.ViewModels;
using Xamarin.Forms;

namespace ProyectoXamarin.Views
{
	/// <summary>
	/// Class NewItemPage.
	/// Implements the <see cref="ContentPage" />
	/// </summary>
	/// <seealso cref="ContentPage" />
	public partial class NewItemPage : ContentPage
	{
		/// <summary>
		/// Gets or sets the item.
		/// </summary>
		/// <value>The item.</value>
		public Item Item { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NewItemPage"/> class.
		/// </summary>
		public NewItemPage()
		{
			InitializeComponent();
			BindingContext = new NewItemViewModel();
		}
	}
}