namespace Simpress.Product.Domain.Entities.DTO
{
    public class ProductDto
    {
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
