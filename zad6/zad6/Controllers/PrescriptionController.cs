using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zad6.context;
using zad6.DTOs;
using zad6.Models;

namespace zad6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionController : ControllerBase
{
    private readonly Z6DbContext _context;

    public PrescriptionController(Z6DbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionRequest request)
    {
        if (request.Medicaments.Count > 10)
        {
            return BadRequest("Max 10 medicaments in one prescription.");
        }

        if (request.DueDate < request.Date)
        {
            return BadRequest("DueDate must be greater or equal to Date.");
        }

        var patient = await _context.Patients.FindAsync(request.Patient.IdPatient);
        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                Birthdate = request.Patient.Birthdate
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = request.IdDoctor
        };

        foreach (var medicament in request.Medicaments)
        {
            var dbMedicament = await _context.Medicaments.FindAsync(medicament.IdMedicament);
            if (dbMedicament == null)
            {
                return NotFound($"Medicament with id {medicament.IdMedicament} does not exist.");
            }

            prescription.PrescriptionMedicaments.Add(new PrescriptionMedicament
            {
                IdMedicament = medicament.IdMedicament,
                Dose = medicament.Dose,
                Details = medicament.Description
            });
        }

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        return Ok(prescription);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientData(int id)
    {
        var patient = await _context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .FirstOrDefaultAsync(p => p.IdPatient == id);

        if (patient == null)
        {
            return NotFound();
        }

        var result = new
        {
            patient.IdPatient,
            patient.FirstName,
            patient.LastName,
            patient.Birthdate,
            Prescriptions = patient.Prescriptions.Select(pr => new
            {
                pr.IdPrescription,
                pr.Date,
                pr.DueDate,
                Medicaments = pr.PrescriptionMedicaments.Select(pm => new
                {
                    pm.IdMedicament,
                    pm.Medicament.Name,
                    pm.Dose,
                    pm.Medicament.Description
                }),
                Doctor = new
                {
                    pr.Doctor.IdDoctor,
                    pr.Doctor.FirstName,
                    pr.Doctor.LastName
                }
            }).OrderBy(pr => pr.DueDate)
        };

        return Ok(result);
    }
}