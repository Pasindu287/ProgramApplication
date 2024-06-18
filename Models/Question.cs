namespace ProgramApplication.Models
{
    public class Question
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public List<string> Options { get; set; } // For Dropdown and MultipleChoice
    }
}
