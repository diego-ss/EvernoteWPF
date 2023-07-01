using EvernoteWPF.Model;
using System;
using System.Windows.Input;

namespace EvernoteWPF.ViewModel.Commands
{
    public class NewNoteCommand : ICommand
    {
        public NotesViewModel NotesViewModel { get; set; }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public NewNoteCommand(NotesViewModel notesViewModel)
        {
            NotesViewModel = notesViewModel;
        }

        public bool CanExecute(object parameter)
        {
            Notebook notebook = parameter as Notebook;

            return notebook != null;
        }

        public void Execute(object parameter)
        {
            Notebook notebook = (Notebook)parameter;
            NotesViewModel.CreateNote(notebook.Id);
        }
    }
}
