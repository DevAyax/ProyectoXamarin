// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 11-29-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="EmailValidatorBehavior.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ProyectoXamarin.Behaviors
{
	using System;
	using System.Text.RegularExpressions;
	using Xamarin.Forms;

	/// <summary>
	/// Class EmailValidatorBehavior.
	/// Implements the <see cref="Xamarin.Forms.Behavior{Xamarin.Forms.Entry}" />
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Behavior{Xamarin.Forms.Entry}" />
	public class EmailValidatorBehavior : Behavior<Entry>
	{
		/// <summary>
		/// The email regex
		/// </summary>
		private const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
			@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

		/// <summary>
		/// The is valid property key
		/// </summary>
		private static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmailValidatorBehavior), false);

		/// <summary>
		/// The is valid property
		/// </summary>
		public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

		/// <summary>
		/// Returns true if ... is valid.
		/// </summary>
		/// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
		public bool IsValid
		{
			get { return (bool) base.GetValue(IsValidProperty); }
			private set { base.SetValue(IsValidPropertyKey, value); }
		}

		/// <summary>
		/// Attaches to the superclass and then calls the <see cref="M:Xamarin.Forms.Behavior`1.OnAttachedTo(`0)" /> method on this object.
		/// </summary>
		/// <param name="bindable">The bindable object to which the behavior was attached.</param>
		/// <remarks>To be added.</remarks>
		protected override void OnAttachedTo(Entry bindable)
		{
			bindable.TextChanged += HandleTextChanged;
			base.OnAttachedTo(bindable);
		}

		/// <summary>
		/// Handles the text changed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
		private void HandleTextChanged(object sender, TextChangedEventArgs e)
		{
			IsValid = (Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
			((Entry) sender).TextColor = IsValid ? Color.Default : Color.Red;
		}

		/// <summary>
		/// Calls the <see cref="M:Xamarin.Forms.Behavior`1.OnDetachingFrom(`0)" /> method and then detaches from the superclass.
		/// </summary>
		/// <param name="bindable">The bindable object from which the behavior was detached.</param>
		/// <remarks>To be added.</remarks>
		protected override void OnDetachingFrom(Entry bindable)
		{
			bindable.TextChanged -= HandleTextChanged;
			base.OnDetachingFrom(bindable);
		}
	}
}