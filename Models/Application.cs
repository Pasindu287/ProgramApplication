namespace ProgramApplication.Models
{
    public class Application
    {
        public string Id { get; set; }
        public string ProgramId { get; set; }
        public List<Question> Questions { get; set; }
    }
}
