using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class DoctorsController : Controller
{
    private readonly ApplicationDbContext _context;

    public DoctorsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // Add default 6 doctors if none exist
        if (!_context.Doctors.Any())
        {
            _context.Doctors.AddRange(
                new Doctor { FullName = "Dr Smith", Specialty = "General" },
                new Doctor { FullName = "Dr Ndlovu", Specialty = "Pediatrics" },
                new Doctor { FullName = "Dr Patel", Specialty = "Cardiology" },
                new Doctor { FullName = "Dr Okoro", Specialty = "Neurology" },
                new Doctor { FullName = "Dr Kim", Specialty = "Dermatology" },
                new Doctor { FullName = "Dr Lopez", Specialty = "Orthopedics" }
            );
            _context.SaveChanges();
        }

        var doctors = _context.Doctors.ToList();
        return View(doctors);
    }
}
