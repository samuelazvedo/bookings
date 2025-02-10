namespace backend.DTOs
{
    public class SuiteDTO
    {
        public int Id { get; set; }
        public int MotelId { get; set; }
        public string SuiteName { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
    }
}