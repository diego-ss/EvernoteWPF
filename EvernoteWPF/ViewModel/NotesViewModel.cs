﻿using EvernoteWPF.Model;
using EvernoteWPF.ViewModel.Commands;
using EvernoteWPF.ViewModel.Helpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

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

        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public NotesViewModel()
        {
            NewNoteCommand = new NewNoteCommand(this);
            NewNotebookCommand = new NewNotebookCommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            GetNotebooks();
        }


        public void CreateNote(int notebookId)
        {
            Note note = new Note
            {
                NotebookId = notebookId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Title = $"Note for {DateTime.Now.ToString()}"
            };

            DatabaseHelper.Insert(note);
            GetNotes();
        }

        public void CreateNotebook()
        {
            Notebook notebook = new Notebook
            {
                Name = "New notebook"
            };

            DatabaseHelper.Insert(notebook);
            GetNotebooks();
        }

        private void GetNotebooks()
        {
            var list = DatabaseHelper.ListItems<Notebook>();

            Notebooks.Clear();
            list.ForEach(notebook =>
            {
                Notebooks.Add(notebook);
            });
        }

        private void GetNotes()
        {
            if(SelectedNotebook != null)
            {
                var list = DatabaseHelper.ListItems<Note>()
                    .Where(n => n.NotebookId == selectedNotebook.Id).ToList();

                Notes.Clear();
                list.ForEach(note =>
                {
                    Notes.Add(note);
                });
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
