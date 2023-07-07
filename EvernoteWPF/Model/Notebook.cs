namespace EvernoteWPF.Model
{
    public class HasId
    {
        public string Id { get; set; }
    }

    public class Notebook : HasId
    {
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}
