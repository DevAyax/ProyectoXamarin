// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 12-05-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="NumericValidationBehavior.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


namespace ProyectoXamarin.Behaviors
{
	using System.Linq;
	using Xamarin.Forms;

	/// <summary>
	/// Class NumericValidationBehavior.
	/// Implements the <see cref="Xamarin.Forms.Behavior{Xamarin.Forms.Entry}" />
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Behavior{Xamarin.Forms.Entry}" />
	public class NumericValidationBehavior : Behavior<Entry>
	{
		/// <summary>
		/// Called when [attached to].
		/// </summary>
		/// <param name="entry">The entry.</param>
		protected override void OnAttachedTo(Entry entry)
		{
			entry.TextChanged += OnEntryTextChanged;
			base.OnAttachedTo(entry);
		}

		/// <summary>
		/// Called when [detaching from].
		/// </summary>
		/// <param name="entry">The entry.</param>
		protected override void OnDetachingFrom(Entry entry)
		{
			entry.TextChanged -= OnEntryTextChanged;
			base.OnDetachingFrom(entry);
		}

		/// <summary>
		/// Handles the <see cref="E:EntryTextChanged" /> event.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="args">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
		private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
		{
			if (!string.IsNullOrWhiteSpace(args.NewTextValue))
			{
				bool isValid = args.NewTextValue.ToCharArray().All(x => char.IsDigit(x)); //Make sure all characters are numbers

				((Entry) sender).Text = isValid ? args.NewTextValue : args.NewTextValue.Remove(args.NewTextValue.Length - 1);
			}
		}
	}
}