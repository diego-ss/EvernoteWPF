using EvernoteWPF.Model;
using EvernoteWPF.ViewModel.Commands;
using EvernoteWPF.ViewModel.Helpers;
using System;
using System.Collections.ObjectModel;

namespace EvernoteWPF.ViewModel
{
    public class NotesViewModel
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }
        public ObservableCollection<Note> Notes { get; set; }

        private Note selectedNotebook;

        public Note SelectedNotebook
        {
            get { return selectedNotebook; }
            set
            {
                selectedNotebook = value;
                //TODO: get notes
            }
        }

        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }

        public NotesViewModel()
        {
            NewNoteCommand = new NewNoteCommand(this);
            NewNotebookCommand = new NewNotebookCommand(this);
        }

        public void CreateNote(int notebookId)
        {
            Note note = new Note
            {
                NotebookId = notebookId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Title = "New note"
            };

            DatabaseHelper.Insert(note);
        }

        public void CreateNotebook()
        {
            Notebook notebook = new Notebook
            {
                Name = "New notebook"
            };

            DatabaseHelper.Insert(notebook);

        }
    }
}
