// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 11-23-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="App.xaml.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.IO;
using System.Threading.Tasks;
using ProyectoXamarin.Data;
using ProyectoXamarin.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProyectoXamarin
{
	/// <summary>
	/// Class App.
	/// Implements the <see cref="Application" />
	/// </summary>
	/// <seealso cref="Application" />
	public partial class App : Application
	{
		/// <summary>
		/// The database
		/// </summary>
		private static DataBase database;

		/// <summary>
		/// Gets the database path.
		/// </summary>
		/// <value>The database path.</value>
		public static string DbPath { get; } =
			Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal)
				, "MyDataBase.db");

		/// <summary>
		/// Gets the data base.
		/// </summary>
		/// <value>The data base.</value>
		public static DataBase DataBase
		{
			get
			{
				if (database == null)
				{
					return database = new DataBase(DbPath);
				}
				else
				{
					return database;
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="App"/> class.
		/// </summary>
		public App()
		{
			InitializeComponent();
			var isLogged = Preferences.ContainsKey(SesionData.SessionKey);
			if (isLogged)
				MainPage = new AppShell();
			else
				MainPage = new NavigationPage(new LoginPage());
		}

		/// <summary>
		/// Application developers override this method to perform actions when the application starts.
		/// </summary>
		/// <remarks>To be added.</remarks>
		protected override void OnStart()
		{
		}

		/// <summary>
		/// Application developers override this method to perform actions when the application enters the sleeping state.
		/// </summary>
		/// <remarks>To be added.</remarks>
		protected override void OnSleep()
		{
		}

		/// <summary>
		/// Application developers override this method to perform actions when the application resumes from a sleeping state.
		/// </summary>
		/// <remarks>To be added.</remarks>
		protected override void OnResume()
		{
		}
	}
}