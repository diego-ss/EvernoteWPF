using System;

namespace EvernoteWPF.Model
{
    public class Note : HasId
    {
        public string NotebookId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}
        public string FileLocation { get; set; }
    }
}
