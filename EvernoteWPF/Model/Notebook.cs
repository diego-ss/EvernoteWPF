using SQLite;

namespace EvernoteWPF.Model
{
    public class Notebook
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public string UserId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
