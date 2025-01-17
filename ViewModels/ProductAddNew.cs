using System.ComponentModel.DataAnnotations;

public class ProductAddNew
{
    public int id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string name { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
    public double price { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string description { get; set; }

    [StringLength(200, ErrorMessage = "Short description cannot exceed 200 characters.")]
    public string shortDescription { get; set; }

    [Required(ErrorMessage = "Quantity is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive value.")]
    public int quantity { get; set; }

    [Required(ErrorMessage = "Image link is required.")]
    [Url(ErrorMessage = "Please provide a valid URL.")]
    public string imgLink { get; set; }
}
