using StateManagments.Models.Models;

namespace StateManagements.Models.Models;

public class Category
{
    public Category()
    {
        this.Products = new HashSet<Product>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public virtual ICollection<Food>? Foods { get; set; }

    public virtual ICollection<Product>? Products { get; set; }
}
