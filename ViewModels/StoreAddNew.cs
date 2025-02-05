using System.ComponentModel.DataAnnotations;

public class StoreAddNew
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên là bắt buộc.")]
    [StringLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Alias là bắt buộc.")]
    [StringLength(50, ErrorMessage = "Alias không được vượt quá 50 ký tự.")]
    public string Alias { get; set; }

    [Required(ErrorMessage = "Vĩ độ là bắt buộc.")]
    [RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "Vĩ độ không hợp lệ.")]
    public string Latitude { get; set; }

    [Required(ErrorMessage = "Kinh độ là bắt buộc.")]
    [RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "Kinh độ không hợp lệ.")]
    public string Longtitude { get; set; }

    [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
    public string Description { get; set; }

    [Url(ErrorMessage = "Đường dẫn ảnh không hợp lệ.")]
    public string Image { get; set; }

    public bool Deleted { get; set; }
}
