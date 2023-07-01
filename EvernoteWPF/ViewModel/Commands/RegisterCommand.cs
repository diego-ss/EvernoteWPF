using System;
using System.Windows.Input;

namespace EvernoteWPF.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        public LoginViewModel LoginViewModel { get; set; }
        public event EventHandler CanExecuteChanged;

        public RegisterCommand(LoginViewModel loginViewModel)
        {
            LoginViewModel = loginViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //TODO: login 
        }
    }
}
