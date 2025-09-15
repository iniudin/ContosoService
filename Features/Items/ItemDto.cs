namespace ContosoService.Features.Items;

using System.ComponentModel.DataAnnotations;

public class CreateItemDto
{
    [Required(ErrorMessage = "Name is required")]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
    [MaxLength(100, ErrorMessage = "Name must be at most 100 characters long")]
    public string Name { get; set; } = default!;
    [Required(ErrorMessage = "Price is required")]
    [Range(0, 10000, ErrorMessage = "Price must be between 0 and 10000")]
    public int Price { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "ParentId must be a positive integer")]
    public int? ParentId { get; set; }
}

public class UpdateItemDto
{
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
    [MaxLength(100, ErrorMessage = "Name must be at most 100 characters long")]
    public string? Name { get; set; }
    [Range(0, 10000, ErrorMessage = "Price must be between 0 and 10000")]
    public int? Price { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "ParentId must be a positive integer")]
    public int? ParentId { get; set; }
}
