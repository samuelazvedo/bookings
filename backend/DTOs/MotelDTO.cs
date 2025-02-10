namespace backend.DTOs
{
    public class MotelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<SuiteDTO> Suites { get; set; } = new();
        public List<ImageDTO> Images { get; set; } = new();
    }
}