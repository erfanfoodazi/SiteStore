namespace Application.UseCases.Products.DTOs
{
    public class RatingResultDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
