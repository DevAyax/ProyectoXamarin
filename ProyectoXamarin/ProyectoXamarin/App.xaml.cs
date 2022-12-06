using System;
using System.IO;
using ProyectoXamarin.Data;
using Xamarin.Forms;

namespace ProyectoXamarin
{
	public partial class App : Application
	{
		private static DataBase database;

		public static string DbPath { get; } =
			Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal)
				, "MyDataBase.db");

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
		public App()
		{
			InitializeComponent();
			MainPage = new AppShell();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
