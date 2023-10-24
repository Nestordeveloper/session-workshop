#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace session_workshop;
public class User
{
    [Required(ErrorMessage = "Name is required")]
    [MinLength(4, ErrorMessage = "Name must be at least 4 characters")]
    public string UserName { get; set; }
}