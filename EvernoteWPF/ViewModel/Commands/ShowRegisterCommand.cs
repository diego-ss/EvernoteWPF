using System;
using System.Windows.Input;

namespace EvernoteWPF.ViewModel.Commands
{
    public class ShowRegisterCommand : ICommand
    {
        public LoginViewModel LoginViewModel { get; set; }
        public event EventHandler CanExecuteChanged;

        public ShowRegisterCommand(LoginViewModel loginViewModel)
        {
            LoginViewModel = loginViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            LoginViewModel.SwitchViews(); 
        }
    }
}
