namespace AppUtbildning.Models;

// Det admin ser på sin profilsida: en snabb översikt över anmälningar och studenter
public class AdminOverview
{
    public int TotalRegistrations { get; set; }
    public int StudentAccounts { get; set; }
    public Dictionary<string, int> RegistrationsPerCourse { get; set; } = new();
    public List<Register> Latest { get; set; } = new();
}
