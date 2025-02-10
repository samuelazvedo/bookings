namespace backend.DTOs
{
    public class ImageDTO
    {
        public int Id { get; set; }
        public string Path { get; set; } = string.Empty;
        public int SuiteId { get; set; }
    }
}