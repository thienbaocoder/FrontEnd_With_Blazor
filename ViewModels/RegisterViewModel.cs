using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Windows.Markup;
public class RegisterViewModel
{
    [Required(ErrorMessage = "Username cannot be blank")]
    [StringLength(maximumLength: 50, ErrorMessage = "UserName from 6 to 50 characters!", MinimumLength = 6)]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Password cannot be blank")]
    public string Password { get; set; }
    [Required(ErrorMessage = "Email cannot be blank")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email is invalid !")]
    public string Email { get; set; }
    [Required(ErrorMessage = "FullName cannot be blank")]
    public string FullName { get; set; }
    public string Gender { get; set; } = "Male";
    public string Country { get; set; } = "VN";
}



public class Country
{
    public string Id { get; set; }
    public string Name { get; set; }
}


public static class ListCountry
{
    public static List<Country> Values = new List<Country>();
    static ListCountry()
    {
        Values.Add(new Country { Id = "VN", Name = "Viet Nam" });
        Values.Add(new Country { Id = "CN", Name = "Trung Quoc" });
        Values.Add(new Country { Id = "EN", Name = "Anh" });
    }

    public static void AddCountry(Country newCountry)
    {
        Values.Add(newCountry);
    }
}