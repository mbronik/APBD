using System.Runtime.InteropServices.JavaScript;

namespace zad6.model;

public class Patient
{
    public int PatientId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birth { get; set; }
}