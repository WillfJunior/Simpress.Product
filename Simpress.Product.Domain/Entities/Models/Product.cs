namespace Simpress.Product.Domain.Entities.Models
{
    public class Product : BaseEntity
    {
        public Product(string? description, decimal price, int categoryId)
        {
            Description = description;
            Price = price;
            CategoryId = categoryId;
        }

        public Product(int id,string? description, decimal price, int categoryId)
        {
            Description = description;
            Price = price;
            CategoryId = categoryId;
            Id = id;
        }

        public void UpdateProduct(string? description, decimal price, int categoryId) 
        {
            Description = description;
            Price = price;
            CategoryId = categoryId;
        }

        public string? Description { get; private set; }
        public decimal Price { get; private set; }
        public int CategoryId { get; private set; }

        public virtual Category? Category{ get; set; }
    }
}
