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
            return true;
        }

        public void Execute(object parameter)
        {
            //TODO: Create new notebook
        }
    }
}
