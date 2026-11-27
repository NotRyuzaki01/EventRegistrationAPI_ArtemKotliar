using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_ArtemKotliar.DTOs;

/// <summary>
/// Data Transfer Object for creating the Registration.
/// </summary>
public class CreateRegistrationDto
{
    [Required(ErrorMessage = "Student name is required.")]
    [StringLength(100,
        ErrorMessage = "Student name cannot exceed 100 characters.")]
    public string StudentName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = string.Empty;
}