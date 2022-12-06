using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace ProyectoXamarin.Models.Users
{
	[Table("User")]
	public class User : BaseEntity, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		private string email;
		private string password;

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
		
		public int? CarId { get; set; }
		
	}
}
