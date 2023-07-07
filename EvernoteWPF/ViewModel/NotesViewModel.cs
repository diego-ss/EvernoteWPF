using EvernoteWPF.Model;
using EvernoteWPF.ViewModel.Commands;
using EvernoteWPF.ViewModel.Helpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace EvernoteWPF.ViewModel
{
    public class NotesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }
        public ObservableCollection<Note> Notes { get; set; }

        private Notebook selectedNotebook;

        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set
            {
                selectedNotebook = value;
                OnPropertyChanged("SelectedNotebook");
                GetNotes();
            }
        }

        private Note selectedNote;

        public Note SelectedNote
        {
            get { return selectedNote; }
            set { 
                selectedNote = value;
                OnPropertyChanged(nameof(SelectedNote));
                SelectedNoteChanged?.Invoke(this, new EventArgs());
            }
        }


        private Visibility editNotebookTextBoxVisibility;

        public Visibility EditNotebookTextBoxVisibility
        {
            get { return editNotebookTextBoxVisibility; }
            set { 
                editNotebookTextBoxVisibility = value; 
                OnPropertyChanged(nameof(EditNotebookTextBoxVisibility));
            }
        }


        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
        public EditCommand EditCommand { get; set; }
        public EndEditingCommand EndEditingCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler SelectedNoteChanged;

        public NotesViewModel()
        {
            NewNoteCommand = new NewNoteCommand(this);
            NewNotebookCommand = new NewNotebookCommand(this);
            EditCommand = new EditCommand(this);
            EndEditingCommand = new EndEditingCommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            EditNotebookTextBoxVisibility = Visibility.Collapsed;

            GetNotebooks();
        }


        public async void CreateNote(string notebookId)
        {
            Note note = new Note
            {
                NotebookId = notebookId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Title = $"Note for {DateTime.Now.ToString()}"
            };

            await DatabaseHelper.Insert(note);
            GetNotes();
        }

        public async void CreateNotebook()
        {
            Notebook notebook = new Notebook
            {
                Name = "New notebook",
                UserId = App.UserId
            };

            await DatabaseHelper.Insert(notebook);
            GetNotebooks();
        }

        public async void GetNotebooks()
        {
            var list = (await DatabaseHelper.ListItems<Notebook>()).Where(n => n.UserId == App.UserId).ToList();

            Notebooks.Clear();
            list.ForEach(notebook =>
            {
                Notebooks.Add(notebook);
            });
        }

        private async void GetNotes()
        {
            if(SelectedNotebook != null)
            {
                var list = (await DatabaseHelper.ListItems<Note>())?.Where(n => n.NotebookId == selectedNotebook.Id).ToList();

                Notes.Clear();
                list?.ForEach(note =>
                {
                    Notes.Add(note);
                });
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void StartEditing()
        {
            EditNotebookTextBoxVisibility = Visibility.Visible;
        }

        public void StopEditing(Notebook notebook)
        {
            EditNotebookTextBoxVisibility = Visibility.Collapsed;
            DatabaseHelper.Update(notebook);
            GetNotebooks();
        }
    }
}
