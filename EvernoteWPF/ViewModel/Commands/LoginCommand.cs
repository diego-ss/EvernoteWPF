using EvernoteWPF.Model;
using System;
using System.Windows.Input;

namespace EvernoteWPF.ViewModel.Commands
{
    public class LoginCommand : ICommand
    {
        public LoginViewModel LoginViewModel { get; set; }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public LoginCommand(LoginViewModel loginViewModel)
        {
            LoginViewModel = loginViewModel;
        }

        public bool CanExecute(object parameter)
        {
            User user = parameter as User;

            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                return false;

            return true;
        }

        public void Execute(object parameter)
        {
            LoginViewModel.Login(); 
        }
    }
}
