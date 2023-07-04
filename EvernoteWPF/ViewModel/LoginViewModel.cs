using EvernoteWPF.Model;
using EvernoteWPF.ViewModel.Commands;
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
			set { user = value; }
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

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
