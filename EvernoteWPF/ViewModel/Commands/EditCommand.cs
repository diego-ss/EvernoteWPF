using System;
using System.Windows.Input;

namespace EvernoteWPF.ViewModel.Commands
{
    public class EditCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public NotesViewModel NotesViewModel { get; private set; }

        public EditCommand(NotesViewModel notesViewModel)
        {
            NotesViewModel = notesViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            NotesViewModel.StartEditing();
        }
    }
}
