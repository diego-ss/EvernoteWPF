using EvernoteWPF.Model;
using EvernoteWPF.ViewModel.Commands;
using System.Collections.ObjectModel;

namespace EvernoteWPF.ViewModel
{
    public class NotesViewModel
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }
		
		private Note selectedNotebook;

		public Note SelectedNotebook
		{
			get { return selectedNotebook; }
			set { 
				selectedNotebook = value; 
				//TODO: get notes
			}
		}

		public ObservableCollection<Note> Notes { get; set; }
		public NewNotebookCommand NewNotebookCommand { get; set; }
		public NewNoteCommand NewNoteCommand { get; set; }

        public NotesViewModel()
        {
			NewNoteCommand = new NewNoteCommand(this);
			NewNotebookCommand = new NewNotebookCommand(this);
        }
    }
}
