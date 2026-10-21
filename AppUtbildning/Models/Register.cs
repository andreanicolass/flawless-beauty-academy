using System;
using System.ComponentModel.DataAnnotations;
namespace AppUtbildning.Models;
/* Använde mig av delat material på canvas, endast att jag lade in mina egna värden*/

public class Register
{
    // Kurserna som går att anmäla sig till. Namnen matchar vyerna i Views/Home.
    public static readonly string[] Courses = { "Fillers", "Botox", "Microneedling" };

    public int Id { get; set; }

    [Display(Name = "Förnamn")]
    [Required(ErrorMessage = "Fyll i ditt förnamn.")]
    [StringLength(50, ErrorMessage = "Förnamnet får vara högst {1} tecken.")]
    public string FirstName { get; set; } = "";

    [Display(Name = "Efternamn")]
    [Required(ErrorMessage = "Fyll i ditt efternamn.")]
    [StringLength(50, ErrorMessage = "Efternamnet får vara högst {1} tecken.")]
    public string LastName { get; set; } = "";

    [Display(Name = "Adress")]
    [Required(ErrorMessage = "Fyll i din adress.")]
    [StringLength(50, ErrorMessage = "Adressen får vara högst {1} tecken.")]
    public string Address { get; set; } = "";

    [Display(Name = "E-post")]
    [Required(ErrorMessage = "Fyll i din e-postadress.")]
    [EmailAddress(ErrorMessage = "Ange en giltig e-postadress, t.ex. namn@exempel.se.")]
    [StringLength(50, ErrorMessage = "E-postadressen får vara högst {1} tecken.")]
    public string Email { get; set; } = "";

    [Display(Name = "Telefon")]
    [Required(ErrorMessage = "Fyll i ditt telefonnummer.")]
    [Phone(ErrorMessage = "Ange ett giltigt telefonnummer.")]
    [StringLength(20, ErrorMessage = "Telefonnumret får vara högst {1} tecken.")]
    public string Phone { get; set; } = "";

    [Display(Name = "Utbildning")]
    [Required(ErrorMessage = "Välj vilken utbildning du är intresserad av.")]
    public string? Course { get; set; }

    [Display(Name = "Anmäld")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
