using EvernoteWPF.Model;
using System;
using System.Windows.Input;

namespace EvernoteWPF.ViewModel.Commands
{
    public class EndEditingCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public NotesViewModel NotesViewModel { get; set; }

        public EndEditingCommand(NotesViewModel notesViewModel)
        {
            NotesViewModel = notesViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var notebook = parameter as Notebook;

            if (notebook != null) 
                NotesViewModel.StopEditing(notebook);
        }
    }
}
