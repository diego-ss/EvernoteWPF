using EvernoteWPF.Model;
using System;

namespace EvernoteWPF.ViewModel.Commands
{
    public class NewNoteCommand
    {
        public NotesViewModel NotesViewModel { get; set; }
        public event EventHandler CanExecuteChanged;

        public NewNoteCommand(NotesViewModel notesViewModel)
        {
            NotesViewModel = notesViewModel;
        }

        public bool CanExecute(object parameter)
        {
            Notebook notebook = parameter as Notebook;

            if(notebook != null)
                return true;

            return false;
        }

        public void Execute(object parameter)
        {
            Notebook notebook = (Notebook)parameter;
            NotesViewModel.CreateNote(notebook.Id);
        }
    }
}
