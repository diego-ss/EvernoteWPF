using EvernoteWPF.ViewModel;
using System.Windows;

namespace EvernoteWPF.View
{
    /// <summary>
    /// Lógica interna para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        LoginViewModel viewModel;

        public LoginWindow()
        {
            InitializeComponent();

            viewModel = Resources["viewModel"] as LoginViewModel;
            viewModel.Authenticated += ViewModel_Authenticated;
        }

        private void ViewModel_Authenticated(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
