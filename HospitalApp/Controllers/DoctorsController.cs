using Microsoft.AspNetCore.Mvc;

public class DoctorsController : Controller
{
    private readonly ApplicationDbContext _context;

    public DoctorsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        if (!_context.Doctors.Any())
        {
            _context.Doctors.AddRange(
                new Doctor { FullName = "Dr Smith", Specialty = "General" },
                new Doctor { FullName = "Dr Ndlovu", Specialty = "Pediatrics" }
            );
            _context.SaveChanges();
        }

        return View(_context.Doctors.ToList());
    }
}
