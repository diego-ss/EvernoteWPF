using System;
using System.Windows.Input;

namespace EvernoteWPF.ViewModel.Commands
{
    public class NewNotebookCommand : ICommand
    {
        public NotesViewModel NotesViewModel { get; set; }
        public event EventHandler CanExecuteChanged;

        public NewNotebookCommand(NotesViewModel notesViewModel)
        {
            NotesViewModel = notesViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            NotesViewModel.CreateNotebook();
        }
    }
}
