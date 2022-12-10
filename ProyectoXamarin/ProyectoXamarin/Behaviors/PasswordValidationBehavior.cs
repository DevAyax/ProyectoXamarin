using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace ProyectoXamarin.Behaviors
{
	public class PasswordValidationBehavior : Behavior<Entry>
	{
		private const string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&.])[A-Za-z\d$@$!%*#?&.]{8,}$";
		private static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(PasswordValidationBehavior), false);
		public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

		public bool IsValid
		{
			get { return (bool) base.GetValue(IsValidProperty); }
			private set { base.SetValue(IsValidPropertyKey, value); }
		}

		protected override void OnAttachedTo(Entry bindable)
		{
			bindable.TextChanged += HandleTextChanged;
			base.OnAttachedTo(bindable);
		}

		private void HandleTextChanged(object sender, TextChangedEventArgs e)
		{
			IsValid = false;
			IsValid = (Regex.IsMatch(e.NewTextValue, passwordRegex));
			((Entry) sender).TextColor = IsValid ? Color.Default : Color.Red;
		}

		protected override void OnDetachingFrom(Entry bindable)
		{
			bindable.TextChanged -= HandleTextChanged;
			base.OnDetachingFrom(bindable);
		}
	}
}