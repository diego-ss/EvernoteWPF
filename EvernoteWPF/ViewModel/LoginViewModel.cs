using EvernoteWPF.Model;
using EvernoteWPF.ViewModel.Commands;
using EvernoteWPF.ViewModel.Helpers;
using System;
using System.ComponentModel;
using System.Windows;

namespace EvernoteWPF.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
		private bool _isShowingRegister = false;
		private User user;

		public User User
		{
			get { return user; }
			set { 
				user = value;
				OnPropertyChanged(nameof(User));
			}
		}

		private string username;

		public string Username
		{
			get { return username; }
			set { 
				username = value;
				User = new User
				{
					Username = username,
					Password = this.Password
				};
                OnPropertyChanged(nameof(Username));
            }
        }

		private string password;

		public string Password
		{
			get { return password; }
			set { 
				password = value;
                User = new User
                {
                    Username = this.Username,
                    Password = password
                };
                OnPropertyChanged(nameof(Password));
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
				User = new User
				{
					Username = this.username,
					Password = this.password,
					Name = name
                };
                OnPropertyChanged(nameof(Name));
            }
        }

        private string lastName;

        public string Lastname
        {
            get { return lastName; }
            set
            {
                lastName = value;
                User = new User
                {
                    Username = this.username,
                    Password = this.password,
                    Name = this.Name,
					Lastname = lastName
                };
                OnPropertyChanged(nameof(Lastname));
            }
        }

        private string confirmPassword;

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                User = new User
                {
                    Username = this.username,
                    Password = this.password,
                    Name = this.Name,
                    Lastname = this.lastName,
                    ConfirmPassword = confirmPassword
                };
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        private Visibility loginVis;
        public Visibility LoginVis
        {
			get { return loginVis; }
			set { 
				loginVis = value; 
				OnPropertyChanged(nameof(LoginVis));
			}
		}

        private Visibility registerVisibility;
        public Visibility RegisterVisibility
        {
            get { return registerVisibility; }
            set
            {
                registerVisibility = value;
                OnPropertyChanged(nameof(RegisterVisibility));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Authenticated;

        public RegisterCommand RegisterCommand { get; set; }
        public ShowRegisterCommand ShowRegisterCommand { get; set; }
        public LoginCommand LoginCommand { get; set; }
		
		public LoginViewModel()
		{
			loginVis = Visibility.Visible;
			registerVisibility = Visibility.Collapsed;

			RegisterCommand = new RegisterCommand(this);
			LoginCommand = new LoginCommand(this);
			ShowRegisterCommand = new ShowRegisterCommand(this);
			User = new User();
		}

		public void SwitchViews()
		{
			_isShowingRegister = !_isShowingRegister;

			if(_isShowingRegister)
			{
				RegisterVisibility = Visibility.Visible;
				LoginVis = Visibility.Collapsed;
			} else
			{
                RegisterVisibility = Visibility.Collapsed;
                LoginVis = Visibility.Visible;
            }
		}

		public async void Login()
		{
            var login = await FirebaseAuthHelper.Login(User);

            if(login)
                Authenticated?.Invoke(this, new EventArgs());
		}

        public async void Register()
        {
            var register = await FirebaseAuthHelper.Register(User);

            if (register)
                Authenticated?.Invoke(this, new EventArgs());
        }

        private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
