// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 11-28-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-12-2022
// ***********************************************************************
// <copyright file="User.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


namespace ProyectoXamarin.Models.Users
{
	using System.ComponentModel;
	using System.Runtime.CompilerServices;
	using SQLite;

	/// <summary>
	/// Class User.
	/// Implements the <see cref="ProyectoXamarin.Models.BaseEntity" />
	/// Implements the <see cref="INotifyPropertyChanged" />
	/// </summary>
	/// <seealso cref="ProyectoXamarin.Models.BaseEntity" />
	/// <seealso cref="INotifyPropertyChanged" />
	[Table("User")]
	public class User : BaseEntity, INotifyPropertyChanged
	{
		/// <summary>
		/// The email
		/// </summary>
		private string email;

		/// <summary>
		/// The password
		/// </summary>
		private string password;

		/// <summary>
		/// The name
		/// </summary>
		private string name;

		/// <summary>
		/// The surname
		/// </summary>
		private string surname;

		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		/// <returns></returns>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Called when [property changed].
		/// </summary>
		/// <param name="name">The name.</param>
		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		/// <summary>
		/// Gets or sets the surname.
		/// </summary>
		/// <value>The surname.</value>
		[MaxLength(50)]
		public string Surname
		{
			get => surname;
			set
			{
				surname = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		[MaxLength(50)]
		public string Name
		{
			get => name;
			set
			{
				name = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>The email.</value>
		[MaxLength(150)]
		public string Email
		{
			get => email;
			set
			{
				email = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>The password.</value>
		[MaxLength(8)]
		[PasswordPropertyText]
		public string Password
		{
			get => password;
			set
			{
				password = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the car identifier.
		/// </summary>
		/// <value>The car identifier.</value>
		public int? CarId { get; set; }
	}
}