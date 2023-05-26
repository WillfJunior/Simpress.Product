namespace Simpress.Product.Domain.Entities.Models
{
    public class Category: BaseEntity
    {
        public string? Description { get; private set; }

        public Category(string? description)
        {
            Description = description;
        }

        public Category(int id, string? description)
        {
            Description = description;
            Id = id;
        }

        public void UpdateCategory(string? description)
        {
            Description = description;
        }

        public virtual ICollection<Product>? Products { get; set; }
    }
}