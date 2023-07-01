using EvernoteWPF.Model;
using EvernoteWPF.ViewModel.Commands;

namespace EvernoteWPF.ViewModel
{
    public class LoginViewModel
    {
		private User user;

		public User User
		{
			get { return user; }
			set { user = value; }
		}

        public RegisterCommand RegisterCommand { get; set; }
        public LoginCommand LoginCommand { get; set; }
		
		public LoginViewModel()
		{
			RegisterCommand = new RegisterCommand(this);
			LoginCommand = new LoginCommand(this);
		}

	}
}
