namespace ProgramApplication.Models
{
    public class ApplicationResponse
    {
        public string Id { get; set; }
        public string ProgramId { get; set; }
        public Dictionary<string, string> Answers { get; set; }
    }
}