// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 11-23-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-12-2022
// ***********************************************************************
// <copyright file="AppShell.xaml.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using ProyectoXamarin.Views;
using Xamarin.Forms;

namespace ProyectoXamarin
{
	/// <summary>
	/// Class AppShell.
	/// Implements the <see cref="Shell" />
	/// </summary>
	/// <seealso cref="Shell" />
	public partial class AppShell : Shell
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AppShell"/> class.
		/// </summary>
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
			Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
		}
	}
}