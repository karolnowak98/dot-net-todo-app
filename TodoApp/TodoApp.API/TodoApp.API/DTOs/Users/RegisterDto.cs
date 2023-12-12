namespace TodoApp.API.DTOs.Users;

public class RegisterDto
{
    [Required(ErrorMessage = "Firstname  is required!")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Lastname is required!")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "Email is required!")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is required!")]
    public string Password { get; set; }
}