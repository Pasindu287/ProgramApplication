namespace ProgramApplication.DTOs
{
public class ApplicationResponseDTO
    {
        public string ProgramId { get; set; }
        public Dictionary<string, string> Answers { get; set; }
    }
}