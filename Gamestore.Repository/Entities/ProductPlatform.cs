using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Entities;

[Table("ProductPlatforms")]
[PrimaryKey(nameof(ProductId), nameof(PlatformId))]
public class ProductPlatform
{
    public Guid ProductId { get; set; }

    public Guid PlatformId { get; set; }

    public Product Product { get; set; }

    public Platform Platform { get; set; }
}
