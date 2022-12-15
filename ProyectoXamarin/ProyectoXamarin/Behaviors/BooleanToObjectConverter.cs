// ***********************************************************************
// Assembly         : ProyectoXamarin
// Author           : Ayax
// Created          : 11-29-2022
//
// Last Modified By : Ayax
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="BooleanToObjectConverter.cs" company="ProyectoXamarin">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ProyectoXamarin.Behaviors
{
	using System;
	using System.Globalization;
	using Xamarin.Forms;

	/// <summary>
	/// Class BooleanToObjectConverter.
	/// Implements the <see cref="IValueConverter" />
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="IValueConverter" />
	public class BooleanToObjectConverter<T> : IValueConverter
	{
		/// <summary>
		/// Gets or sets the false object.
		/// </summary>
		/// <value>The false object.</value>
		public T FalseObject { set; get; }

		/// <summary>
		/// Gets or sets the true object.
		/// </summary>
		/// <value>The true object.</value>
		public T TrueObject { set; get; }

		/// <summary>
		/// Implement this method to convert <paramref name="value" /> to <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="targetType">The type to which to convert the value.</param>
		/// <param name="parameter">A parameter to use during the conversion.</param>
		/// <param name="culture">The culture to use during the conversion.</param>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		public object Convert(object value, Type targetType,
							  object parameter, CultureInfo culture)
		{
			return (bool) value ? this.TrueObject : this.FalseObject;
		}

		/// <summary>
		/// Implement this method to convert <paramref name="value" /> back from <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="targetType">The type to which to convert the value.</param>
		/// <param name="parameter">A parameter to use during the conversion.</param>
		/// <param name="culture">The culture to use during the conversion.</param>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		public object ConvertBack(object value, Type targetType,
								  object parameter, CultureInfo culture)
		{
			return ((T) value).Equals(this.TrueObject);
		}
	}
}